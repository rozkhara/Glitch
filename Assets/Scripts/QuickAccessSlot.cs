using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickAccessSlot : MonoBehaviour
{

    private GameObject QAobject;
    private Inventory inventoryManager;
    public void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>();
        QAobject = inventoryManager.qaObject;
    }

    private void UpdateQuickAccessSlot()
    {
        for (int i = 0; i < 4; i++)
        {
            if (inventoryManager.qaisEmpty[i])
            {
                if (QAobject != null)
                {
                    QAobject.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Text>().text = "";
                    QAobject.transform.GetChild(1).GetChild(i).GetComponent<Image>().sprite = null;
                    QAobject.transform.GetChild(1).GetChild(i).GetComponent<Image>().enabled = false;

                }

            }
            else
            {
                if (QAobject != null)
                {
                    QAobject.transform.GetChild(1).GetChild(i).GetComponent<Image>().enabled = true;
                    if (inventoryManager.quickAccessArray[i].amount > 1)
                    {
                        QAobject.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Text>().text = inventoryManager.quickAccessArray[i].amount.ToString() + " ";
                        QAobject.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Text>().fontSize = 36;
                        QAobject.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Text>().color = Color.green;
                        QAobject.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Text>().alignment = TextAnchor.LowerRight;
                        QAobject.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    }
                    else
                    {
                        QAobject.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Text>().text = "";

                    }
                    switch (inventoryManager.quickAccessArray[i].type)
                    {
                        case Item.Type.Potion10:
                            QAobject.transform.GetChild(1).GetChild(i).GetComponent<Image>().sprite = inventoryManager.sprites[0];
                            break;
                        case Item.Type.Potion30:
                            QAobject.transform.GetChild(1).GetChild(i).GetComponent<Image>().sprite = inventoryManager.sprites[1];
                            break;
                        case Item.Type.Potion50:
                            QAobject.transform.GetChild(1).GetChild(i).GetComponent<Image>().sprite = inventoryManager.sprites[2];
                            break;
                        case Item.Type.Teleport:
                            QAobject.transform.GetChild(1).GetChild(i).GetComponent<Image>().sprite = inventoryManager.sprites[3];
                            break;
                        case Item.Type.MaxHealthUp:
                            QAobject.transform.GetChild(1).GetChild(i).GetComponent<Image>().sprite = inventoryManager.sprites[4];
                            break;
                        case Item.Type.BuffPotion:
                            QAobject.transform.GetChild(1).GetChild(i).GetComponent<Image>().sprite = inventoryManager.sprites[5];
                            break;
                        default: break;
                    }
                }
            }
        }
    }

    public void Update()
    {
        if (QAobject == null)
        {
            QAobject = inventoryManager.qaObject;
        }
        UpdateQuickAccessSlot();
    }

}
