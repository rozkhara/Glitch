using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum Type
    {
        Potion10,
        Potion30,
        Potion50,
        Teleport,
        MaxHealthUp,
        BuffPotion
    }
    public int amount;
    public Type type;

}
