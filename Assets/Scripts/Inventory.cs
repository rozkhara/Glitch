using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Item[,] inventoryArray = new Item[4, 6];
    public Item[] quickAccessArray = new Item[4];
    private int[] qaIndex = new int[4];
    public bool[] qaisEmpty = { true, true, true, true, true };
    public bool[,] isEmpty = { { true, true, true, true, true, true }, { true, true, true, true, true, true }, { true, true, true, true, true, true }, { true, true, true, true, true, true } };

    [SerializeField] private GameObject qaSlotPrefab;
    public GameObject qaObject;

    public List<Sprite> sprites;
    private GameObject itemSlot;
    [SerializeField] private GameObject inventoryAll;
    private Canvas canvas;
    private bool isInvOpen;
    private GameObject instInvObj;
    private GM gameManager;

    public void AddItem(Item item)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (inventoryArray[i, j] != null && inventoryArray[i, j].type == item.type)
                {
                    if (inventoryArray[i, j].amount == 64)
                    {
                        continue;
                    }
                    inventoryArray[i, j].amount += item.amount;
                    return;
                }

            }
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (isEmpty[i, j])
                {
                    inventoryArray[i, j] = item;
                    isEmpty[i, j] = false;
                    return;
                }

            }
        }
        Debug.LogError("Inventory is Full!");
    }

    public void UseItem(int index)
    {
        if (!gameManager.isOnPause)
        {
            int internalIndex = qaIndex[index];
            int j = internalIndex % 6;
            int i = (internalIndex - j) / 6;
            if (!qaisEmpty[index])
            {
                switch (inventoryArray[i, j].type)
                {
                    case Item.Type.Potion10:
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().health += 10;
                        break;
                    case Item.Type.Potion30:
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().health += 30;
                        break;
                    case Item.Type.Potion50:
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().health += 50;
                        break;
                    case Item.Type.Teleport:

                        break;
                    case Item.Type.MaxHealthUp:

                        break;
                    case Item.Type.BuffPotion:

                        break;
                    default:
                        break;
                }
                Debug.Log(inventoryArray[i, j].type);
                //use item
                //                inventoryArray[i,j]
                inventoryArray[i, j].amount--;
                if (inventoryArray[i, j].amount == 0)
                {
                    isEmpty[i, j] = true;
                    qaisEmpty[index] = true;
                    inventoryArray[i, j] = null;
                    quickAccessArray[index] = null;
                    for (int k = 0; k < 4; k++)
                    {
                        if (k == index)
                        {
                            continue;
                        }
                        if (qaIndex[k] == qaIndex[index])
                        {
                            qaIndex[k] = -1;
                            qaisEmpty[k] = true;
                            quickAccessArray[k] = null;
                        }
                    }
                    qaIndex[index] = -1;

                }
            }
            return;
        }
        return;
    }

    public void RemoveAllItem(int i, int j)
    {
        inventoryArray[i, j].amount = 0;
        isEmpty[i, j] = true;
        return;
    }
    public void SwapItem(int index1, int index2)
    {
        int j = index1 % 6;
        int i = (index1 - j) / 6;
        int l = index2 % 6;
        int k = (index2 - l) / 6;
        Item tempItem = inventoryArray[k, l];
        inventoryArray[k, l] = inventoryArray[i, j];
        inventoryArray[i, j] = tempItem;
        if (isEmpty[i, j] && !isEmpty[k, l])
        {
            isEmpty[k, l] = true;
            isEmpty[i, j] = false;
        }
        else if (isEmpty[k, l] && !isEmpty[i, j])
        {
            isEmpty[k, l] = false;
            isEmpty[i, j] = true;
        }
        for (int index = 0; index < 4; index++)
        {
            if (index1 == qaIndex[index])
            {
                qaIndex[index] = index2;
            }
            else if (index2 == qaIndex[index])
            {
                qaIndex[index] = index1;
            }
        }

    }

    /// <summary>
    /// Copies Item into QA array
    /// </summary>
    /// <param name="index1">Normal Inventory Array Index</param>
    /// <param name="index2">QA Array Index</param>
    public void CopyItemToQA(int index1, int index2)
    {
        int j = index1 % 6;
        int i = (index1 - j) / 6;
        Item copiedItem = inventoryArray[i, j];
        quickAccessArray[index2] = copiedItem;
        qaisEmpty[index2] = false;
        qaIndex[index2] = index1;
    }

    public void RemoveItemFromQA(int index)
    {
        qaisEmpty[index] = true;
        qaIndex[index] = -1;
    }

    public void UpdateQA()
    {
        for (int i = 0; i < 4; i++)
        {
            if (qaisEmpty[i])
            {
                if (quickAccessArray[i] != null)
                {
                    quickAccessArray[i] = null;
                    qaIndex[i] = -1;
                }
                if (itemSlot != null)
                {
                    itemSlot.transform.GetChild(24 + i).GetChild(0).GetComponent<Text>().text = "";
                    itemSlot.transform.GetChild(24 + i).GetComponent<Image>().sprite = null;
                    itemSlot.transform.GetChild(24 + i).GetComponent<Image>().enabled = false;
                }

            }
            else
            {
                if (itemSlot != null)
                {
                    itemSlot.transform.GetChild(24 + i).GetComponent<Image>().enabled = true;
                    if (quickAccessArray[i].amount > 1)
                    {
                        itemSlot.transform.GetChild(24 + i).GetChild(0).GetComponent<Text>().text = quickAccessArray[i].amount.ToString() + " ";
                        itemSlot.transform.GetChild(24 + i).GetChild(0).GetComponent<Text>().fontSize = 36;
                        itemSlot.transform.GetChild(24 + i).GetChild(0).GetComponent<Text>().color = Color.green;
                        itemSlot.transform.GetChild(24 + i).GetChild(0).GetComponent<Text>().alignment = TextAnchor.LowerRight;
                        itemSlot.transform.GetChild(24 + i).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);

                    }
                    else
                    {
                        itemSlot.transform.GetChild(24 + i).GetChild(0).GetComponent<Text>().text = "";
                    }
                    switch (quickAccessArray[i].type)
                    {
                        case Item.Type.Potion10:
                            itemSlot.transform.GetChild(24 + i).GetComponent<Image>().sprite = sprites[0];
                            break;
                        case Item.Type.Potion30:
                            itemSlot.transform.GetChild(24 + i).GetComponent<Image>().sprite = sprites[1];
                            break;
                        case Item.Type.Potion50:
                            itemSlot.transform.GetChild(24 + i).GetComponent<Image>().sprite = sprites[2];
                            break;
                        case Item.Type.Teleport:
                            itemSlot.transform.GetChild(24 + i).GetComponent<Image>().sprite = sprites[3];
                            break;
                        case Item.Type.MaxHealthUp:
                            itemSlot.transform.GetChild(24 + i).GetComponent<Image>().sprite = sprites[4];
                            break;
                        case Item.Type.BuffPotion:
                            itemSlot.transform.GetChild(24 + i).GetComponent<Image>().sprite = sprites[5];
                            break;
                        default: break;
                    }
                }

            }
        }
    }

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        GameObject tempObject = GameObject.Find("Canvas");
        
        
        if (tempObject != null)
        {
            canvas = tempObject.GetComponent<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("Could not locate Canvas component on " + tempObject.name);
            }
        }
        isInvOpen = false;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GM>();

    }

    public void Update()
    {
        /*
        if (!isEmpty[0, 0])
        {
            Debug.Log("For Position 0,0, " + inventoryArray[0, 0].type + " " + inventoryArray[0, 0].amount);
        }
        if (!isEmpty[0, 1])
        {
            Debug.Log("For Position 0,1, " + inventoryArray[0, 1].type + " " + inventoryArray[0, 1].amount);
        }
        if (!isEmpty[0, 2])
        {
            Debug.Log("For Position 0,2, " + inventoryArray[0, 2].type + " " + inventoryArray[0, 2].amount);
        }
        */
        if (isInvOpen)
        {
            UpdateSprite();
            if (Input.GetKeyDown(KeyCode.I))
            {
                Destroy(instInvObj);
                gameManager.Unpause();
                isInvOpen = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                instInvObj = Instantiate(inventoryAll, canvas.transform);
                gameManager.Pause();
                isInvOpen = true;
                itemSlot = GameObject.Find("ItemSlot");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseItem(3);
        }
        UpdateQA();
        GameObject tempqa = GameObject.FindGameObjectWithTag("QuickAccess");
        if (tempqa == null && canvas != null) 
        {
            qaObject = Instantiate(qaSlotPrefab, canvas.transform);
        }
    }

    private void UpdateSprite()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (!isEmpty[i, j])
                {
                    switch (inventoryArray[i, j].type)
                    {
                        case Item.Type.Potion10:
                            itemSlot.transform.GetChild(i * 6 + j).GetComponent<Image>().enabled = true;
                            if (inventoryArray[i, j].amount > 1)
                            {
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().text = inventoryArray[i, j].amount.ToString() + " ";
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().fontSize = 36;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().color = Color.green;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().alignment = TextAnchor.LowerRight;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);

                            }
                            else
                            {
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().text = "";
                            }
                            itemSlot.transform.GetChild(i * 6 + j).GetComponent<Image>().sprite = sprites[0];
                            break;
                        case Item.Type.Potion30:
                            itemSlot.transform.GetChild(i * 6 + j).GetComponent<Image>().enabled = true;
                            if (inventoryArray[i, j].amount > 1)
                            {
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().text = inventoryArray[i, j].amount.ToString() + " ";
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().fontSize = 36;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().color = Color.green;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().alignment = TextAnchor.LowerRight;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);

                            }
                            else
                            {
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().text = "";
                            }
                            itemSlot.transform.GetChild(i * 6 + j).GetComponent<Image>().sprite = sprites[1];
                            break;
                        case Item.Type.Potion50:
                            itemSlot.transform.GetChild(i * 6 + j).GetComponent<Image>().enabled = true;
                            if (inventoryArray[i, j].amount > 1)
                            {
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().text = inventoryArray[i, j].amount.ToString() + " ";
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().fontSize = 36;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().color = Color.green;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().alignment = TextAnchor.LowerRight;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);

                            }
                            else
                            {
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().text = "";
                            }
                            itemSlot.transform.GetChild(i * 6 + j).GetComponent<Image>().sprite = sprites[2];
                            break;
                        case Item.Type.Teleport:
                            itemSlot.transform.GetChild(i * 6 + j).GetComponent<Image>().enabled = true;
                            if (inventoryArray[i, j].amount > 1)
                            {
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().text = inventoryArray[i, j].amount.ToString() + " ";
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().fontSize = 36;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().color = Color.green;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().alignment = TextAnchor.LowerRight;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);

                            }
                            else
                            {
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().text = "";
                            }
                            itemSlot.transform.GetChild(i * 6 + j).GetComponent<Image>().sprite = sprites[4];
                            break;
                        case Item.Type.MaxHealthUp:
                            itemSlot.transform.GetChild(i * 6 + j).GetComponent<Image>().enabled = true;
                            if (inventoryArray[i, j].amount > 1)
                            {
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().text = inventoryArray[i, j].amount.ToString() + " ";
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().fontSize = 36;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().color = Color.green;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().alignment = TextAnchor.LowerRight;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);

                            }
                            else
                            {
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().text = "";
                            }
                            itemSlot.transform.GetChild(i * 6 + j).GetComponent<Image>().sprite = sprites[4];
                            break;
                        case Item.Type.BuffPotion:
                            itemSlot.transform.GetChild(i * 6 + j).GetComponent<Image>().enabled = true;
                            if (inventoryArray[i, j].amount > 1)
                            {
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().text = inventoryArray[i, j].amount.ToString() + " ";
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().fontSize = 36;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().color = Color.green;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().alignment = TextAnchor.LowerRight;
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);

                            }
                            else
                            {
                                itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().text = "";
                            }
                            itemSlot.transform.GetChild(i * 6 + j).GetComponent<Image>().sprite = sprites[5];
                            break;
                        default: break;
                    }
                }
                else
                {
                    itemSlot.transform.GetChild(i * 6 + j).GetChild(0).GetComponent<Text>().text = "";
                    itemSlot.transform.GetChild(i * 6 + j).GetComponent<Image>().sprite = null;
                    itemSlot.transform.GetChild(i * 6 + j).GetComponent<Image>().enabled = false;
                }
            }
        }
    }
}
