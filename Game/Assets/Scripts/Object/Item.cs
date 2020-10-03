using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    public ItemType ItemType = ItemType.PLACEHOLDER;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var itemGrabber = other.GetComponent<PlayerItemGrabber>();
        if (itemGrabber != null)
        {
            itemGrabber.Grab(this);
            Destroy(gameObject);
        }
    }
}

public enum ItemType
{
    PLACEHOLDER,
    CLUB,
    SWORD,
    PICKAXE,
    SLIPPERS,
    LEATHER_BOOTS,
    MAGIC_BOOTS
}
