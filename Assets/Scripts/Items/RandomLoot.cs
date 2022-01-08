using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{


    //item probability in decreasing order
    public int[] woodDropTable =
    {
        60, //item 1
        30, //item 2
        10 //item 3
    };

    public int[] ironDropTable =
    {
        60, //item 1
        30, //item 2
        10 //item 3
    };

    public int[] goldDropTable =
    {
        60, //item 1
        30, //item 2
        10 //item 3
    };

    public int total;
    public int randomNumber;
    public int type;
    private GM gameManager;
    private Inventory inventoryManager;


    // Start is called before the first frame update
    void Awake()
    {

        type = -1;
        tag = this.gameObject.tag;
        if (tag == "woodCrate")
        {
            type = 0;
        }
        else if (tag == "ironCrate")
        {
            type = 1;
        }
        else if (tag == "goldCrate")
        {
            type = 2;
        }
        else
        {
            Debug.LogError("Either RadomLoot.cs is not located under crates, or the crate gameobject does not have proper tag associated");
            return;
        }




    }

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GM>();
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isOnPause)
        {
            //if player position is in proximity
            if (Input.GetKeyDown(KeyCode.A))
            {
                CrateGive(type);
            }
        }
        

    }

    private bool CrateGive(int type)
    {
        if (type == 0)
        {
            foreach (var item in woodDropTable)
            {
                total += item;
            }

            randomNumber = Random.Range(0, total);

            foreach (var weight in woodDropTable)
            {
                if (randomNumber <= weight)
                {
                    //give item
                    Item targetItem = new Item();
                    if (weight == 60)
                    {
                        targetItem.type = Item.Type.Potion10;
                        targetItem.amount = 1;
                    }
                    else if (weight == 30)
                    {
                        targetItem.type = Item.Type.Teleport;
                        targetItem.amount = 1;
                    }
                    else if (weight == 10)
                    {
                        targetItem.type = Item.Type.Potion30;
                        targetItem.amount = 1;
                    }
                    inventoryManager.AddItem(targetItem);
                    Debug.Log("item with weight " + weight + " has been given!");
                    total = 0;
                    Destroy(this.gameObject);
                    return true;
                }
                else
                {
                    randomNumber -= weight;
                }
            }

        }
        else if (type == 1)
        {
            foreach (var item in ironDropTable)
            {
                total += item;
            }

            randomNumber = Random.Range(0, total);

            foreach (var weight in ironDropTable)
            {
                if (randomNumber <= weight)
                {
                    //give item
                    Item targetItem = new Item();
                    if (weight == 60)
                    {
                        targetItem.type = Item.Type.Potion30;
                        targetItem.amount = 2;
                    }
                    else if (weight == 30)
                    {
                        targetItem.type = Item.Type.BuffPotion;
                        targetItem.amount = 1;
                    }
                    else if (weight == 10)
                    {
                        targetItem.type = Item.Type.Potion50;
                        targetItem.amount = 1;
                    }
                    inventoryManager.AddItem(targetItem);
                    Debug.Log("item with weight " + weight + " has been given!");
                    total = 0;
                    Destroy(this.gameObject);
                    return true;
                }
                else
                {
                    randomNumber -= weight;
                }
            }
        }
        else if (type == 2)
        {
            foreach (var item in goldDropTable)
            {
                total += item;
            }

            randomNumber = Random.Range(0, total);

            foreach (var weight in goldDropTable)
            {
                if (randomNumber <= weight)
                {
                    //give item
                    Item targetItem = new Item();
                    if (weight == 60)
                    {
                        targetItem.type = Item.Type.Potion50;
                        targetItem.amount = 2;
                    }
                    else if (weight == 30)
                    {
                        targetItem.type = Item.Type.BuffPotion;
                        targetItem.amount = 2;
                    }
                    else if (weight == 10)
                    {
                        targetItem.type = Item.Type.Teleport;
                        targetItem.amount = 2;
                    }
                    inventoryManager.AddItem(targetItem);
                    Debug.Log("item with weight " + weight + " has been given!");
                    total = 0;
                    Destroy(this.gameObject);
                    return true;
                }
                else
                {
                    randomNumber -= weight;
                }
            }
        }

        Debug.LogError("Unspecified type of crate has been called to give, type was "+ type);
        total = 0;
        return false;


    }
}
