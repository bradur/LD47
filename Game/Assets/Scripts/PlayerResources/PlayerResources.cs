using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerResources : MonoBehaviour
{
    public static PlayerResources main;
    private GameConfig config;
    PlayerInventory inventory;
    bool resetWasCalled = false;

    void Awake()
    {
        main = this;
    }

    public void Init()
    {
        config = Configs.main.Game;
        foreach (PlayerResource resource in config.Resources)
        {
            resource.Value = resource.InitialValue;
            resource.Level = 0;
            resource.TotalXp = 0;
        }
    }

    void Start()
    {
        Reset();
        HUDManager.main.Refresh();
        inventory = Configs.main.PlayerInventory;
    }

    void Update()
    {
    }

    public void Reset()
    {
        config = Configs.main.Game;
        HUDManager.main.Init();
        foreach (PlayerResource resource in config.Resources)
        {
            resource.Reset();
        }
    }

    public void Gain(PlayerResourceType resourceType, int amount)
    {
        PlayerResource resource = config.Resources.Find(r => r.Type == resourceType);
        resource.Gain(amount);
        HUDManager.main.Refresh();
        UIManager.main.ShowBillboardText(amount + "xp", Tools.GetPlayerPositionWithOffset(), resource.Icon, resource.Color);
    }

    public bool SpendEnergy(int amount)
    {
        bool spent = Spend(PlayerResourceType.Energy, amount);
        return spent;
    }

    public void DialogFinished () {

    }

    public bool Spend(PlayerResourceType resourceType, int amount)
    {
        bool success = config.Resources
            .Find(resource => resource.Type == resourceType)
            .Spend(amount);
        HUDManager.main.Refresh();
        ResetIfNeeded();
        return success;
    }

    private void ResetIfNeeded() {
        bool reset = config.Resources.Where(resource => !resource.IsSkill).Any(resource => resource.Value <= 0);
        if (reset && !resetWasCalled) {
            LoopManager.main.Reset(true);
            resetWasCalled = true;
        }
    }

    public float GetDistanceTraveledPerEnergy()
    {
        var efficiency = getBootsEfficiency(inventory.GetBoots()) * getWalkingSkillEfficiency();
        return efficiency * config.BaseDistanceWalkedPerEnergy;
    }

    public float GetMoveSpeed()
    {
        InventoryItem boots = inventory.GetBoots();
        var moveSpeedIndex = boots == null ? 0 : boots.ItemLevel + 1;
        return config.MoveSpeeds[moveSpeedIndex];
    }

    public int GetEnergyPerStrike()
    {
        var skillLevel = config.Resources
            .Find(resource => resource.Type == PlayerResourceType.StrengthSkill)
            .Level;
        return Mathf.CeilToInt(config.BaseEnergySpentByHit / (1.0f + 0.5f * skillLevel));
    }

    private float getBootsEfficiency(InventoryItem item)
    {
        // 0 = no boots, 1 = slippers (itemlevel = 0) etc.
        InventoryItem boots = inventory.GetBoots();
        var index = boots == null ? 0 : boots.ItemLevel + 1;

        return config.BootsEfficiency[index];
    }

    private float getWalkingSkillEfficiency()
    {
        var skillLevel = config.Resources
            .Find(resource => resource.Type == PlayerResourceType.AthleticsSkill)
            .Level;

        return 1.0f + skillLevel * 0.2f;
    }


}

[System.Serializable]
public class PlayerResource
{
    [SerializeField]
    private Sprite resourceIcon;
    public Sprite Icon { get { return resourceIcon; } }
    [SerializeField]
    private Color resourceColor;
    public Color Color { get { return resourceColor; } }

    [SerializeField]
    private PlayerResourceType resourceType;
    public PlayerResourceType Type { get { return resourceType; } }

    [SerializeField]
    private int initialValue;
    public int InitialValue { get { return initialValue; } }

    [SerializeField]
    private bool isSkill = false;
    public bool IsSkill { get { return isSkill; } }

    private int currentValue;
    public int Value { get { return currentValue; } set { currentValue = value; } }


    private int totalXp;
    public int TotalXp { get { return totalXp; } set { totalXp = value; } }

    [SerializeField]
    private int baseXpPerLevel = 10;
    public int XpPerLevel {
        get {
            return baseXpPerLevel + 5 * level;
        }
    }

    private int level = 0;
    public int Level { get { return level; } set { level = value; } }

    public bool Spend(int amount)
    {
        if (currentValue < amount)
        {
            currentValue = 0;
            return false;
        }
        else
        {
            currentValue -= amount;
            return true;
        }
    }

    public void Gain(int amount)
    {
        currentValue += amount;
        if (isSkill) {
            totalXp += amount;
        }
    }

    public void Reset()
    {
        if (isSkill)
        {
        }
        else
        {
            currentValue = initialValue;
        }
    }
}

public enum PlayerResourceType
{
    None,
    Energy,
    Health,
    AthleticsSkill,
    StrengthSkill
}
