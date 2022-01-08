using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Item[,] inventoryArray = new Item[4, 6];
    public bool[,] isEmpty = { { true, true, true, true, true, true }, { true, true, true, true, true, true }, { true, true, true, true, true, true }, { true, true, true, true, true, true } };
    public int[,] typeArray = new int[4, 6];
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
    }

    public void UseItem(int i, int j)
    {
        inventoryArray[i, j].amount--;
        if (inventoryArray[i, j].amount == 0)
        {
            isEmpty[i, j] = true;
            inventoryArray[i, j] = null;
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
    }
    public void Start()
    {
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
                gameManager.isOnPause = false;
                isInvOpen = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                instInvObj = Instantiate(inventoryAll, canvas.transform);
                gameManager.isOnPause = true;
                isInvOpen = true;
                itemSlot = GameObject.Find("ItemSlot");
            }
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
