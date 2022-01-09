using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // Start is called before the first frame update
    public Vector3 playerPos;
    public Vector3 playerScale;
    public Item[,] inventoryArray = new Item[4, 6];
    public Item[] quickAccessArray;

}
