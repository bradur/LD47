using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerResources : MonoBehaviour
{
    public static PlayerResources main;
    private GameConfig config;

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
        }
    }

    public void Reset()
    {
        foreach (PlayerResource resource in config.Resources)
        {
            resource.Reset();
        }
    }

    public void Gain(PlayerResourceType resourceType, int amount)
    {
        config.Resources
            .Find(resource => resource.Type == resourceType)
            .Gain(amount);
        HUDManager.main.Refresh();
    }

    public bool SpendEnergy(int amount)
    {
        return Spend(PlayerResourceType.Energy, amount);
    }

    public bool Spend(PlayerResourceType resourceType, int amount)
    {
        bool success = config.Resources
            .Find(resource => resource.Type == resourceType)
            .Spend(amount);
        HUDManager.main.Refresh();
        return success;
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


    [SerializeField]
    private int xpPerLevel = 10;
    public int XpPerLevel { get { return xpPerLevel; } }

    public bool Spend(int amount)
    {
        if (currentValue < amount)
        {
            return false;
        }
        else
        {
            currentValue -= amount;
            return true;
        }
    }

    public void Gain(int amount){
        currentValue += amount;
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
