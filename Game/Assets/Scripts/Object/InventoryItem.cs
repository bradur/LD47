
using System;
using UnityEngine;

[Serializable]
public class InventoryItem {
    [SerializeField]
    private ItemType type;
    public ItemType Type { get { return type; } }
    
    [SerializeField]
    private Sprite itemIcon;
    public Sprite ItemIcon { get { return itemIcon; } }
    
    [SerializeField]
    private int itemLevel;
    public int ItemLevel { get { return itemLevel; } }
    
    [SerializeField]
    private InventorySlot slot;
    public InventorySlot Slot { get { return slot; } }
}

public enum InventorySlot {
    WEAPON,
    BOOTS
}