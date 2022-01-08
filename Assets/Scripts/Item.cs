using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        HealthPotion,
        BuffPotion,
        Teleport,
        MaxHealthUp,
    }

    public ItemType itemType;
    public int amount;
}
