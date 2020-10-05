using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "Game/PlayerInventory", order = 0)]
public class PlayerInventory : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> allItems = new List<InventoryItem>();
    public List<InventoryItem> AllItems { get { return allItems; } }

    [SerializeField]
    private List<InventoryItem> playerItems = new List<InventoryItem>();
    public List<InventoryItem> PlayerItems { get { return playerItems; } }

    public void Init()
    {
        playerItems = new List<InventoryItem>();
    }

    public void GrabItem(ItemType type)
    {
        InventoryItem item = allItems.Where(x => x.Type == type).FirstOrDefault();
        if (item == null)
        {
            Debug.Log("Error: item config is missing item type: " + type.ToString());
            return;
        }

        ItemComment itemComment = Configs.main.UI.ItemComments.Where(ic => ic.Item == item.Type).First();
        if (itemComment != null) {
            UIManager.main.ShowDialog(itemComment.Comment, Tools.GetPlayerPosition());
        }

        playerItems.Add(item);
    }

    public InventoryItem GetBoots()
    {
        return PlayerItems.FindAll(x => x.Slot == InventorySlot.BOOTS)
            .OrderByDescending(x => x.ItemLevel)
            .FirstOrDefault();
    }

    public InventoryItem GetWeapon()
    {
        return PlayerItems.FindAll(x => x.Slot == InventorySlot.WEAPON)
                    .OrderByDescending(x => x.ItemLevel)
                    .FirstOrDefault();
    }
}
