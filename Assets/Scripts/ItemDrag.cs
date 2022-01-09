using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerClickHandler
{
    Vector3 initPos;
    Vector3 endPos;
    Vector3 pointerPos;
    int initIndex;
    int endIndex;
    RectTransform rt;
    Inventory inventoryManager;
    private bool isDraggable;

    public void OnBeginDrag(PointerEventData eventData)
    {
        rt = GetComponent<RectTransform>();
        pointerPos = rt.anchoredPosition;
        initPos = transform.position;
        if (pointerPos.x >= 450 - 50 && pointerPos.x <= 450 + 50)
        {
            isDraggable = false;
        }
        else
        {
            initIndex = transform.GetSiblingIndex();
            isDraggable = true;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            transform.position = Input.mousePosition;

        }
        else
        {

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        rt = GetComponent<RectTransform>();
        endPos = rt.anchoredPosition;
        Debug.Log(endPos);

        int i = 0, j = 0;
        if (endPos.x >= -300 - 50 && endPos.x <= -300 + 50) j = 0;
        else if (endPos.x >= -180 - 50 && endPos.x <= -180 + 50) j = 1;
        else if (endPos.x >= -60 - 50 && endPos.x <= -60 + 50) j = 2;
        else if (endPos.x >= 60 - 50 && endPos.x <= 60 + 50) j = 3;
        else if (endPos.x >= 180 - 50 && endPos.x <= 180 + 50) j = 4;
        else if (endPos.x >= 300 - 50 && endPos.x <= 300 + 50) j = 5;
        else if (endPos.x >= 450 - 50 && endPos.x <= 450 + 50) j = -2;
        else j = -1;
        if (endPos.y <= 180 + 50 && endPos.y >= 180 - 50) i = 0;
        else if (endPos.y <= 60 + 50 && endPos.y >= 60 - 50) i = 1;
        else if (endPos.y <= -60 + 50 && endPos.y >= -60 - 50) i = 2;
        else if (endPos.y <= -180 + 50 && endPos.y >= -180 - 50) i = 3;
        else i = -1;
        if (i == -1 || j == -1)
        {
            endIndex = initIndex;
        }
        else
        {
            if (j != -2)
            {
                endIndex = i * 6 + j;

            }
            else // QA array
            {
                if (isDraggable)
                {
                    endIndex = initIndex;
                    inventoryManager.qaisEmpty[i] = false;
                    inventoryManager.CopyItemToQA(initIndex, i);

                }

            }
        }
        transform.position = initPos;

        if (initIndex != endIndex)
        {
            if (isDraggable)
            {
                inventoryManager.SwapItem(initIndex, endIndex);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        rt = GetComponent<RectTransform>();
        pointerPos = rt.anchoredPosition;
        if (pointerPos.x >= 450 - 50 && pointerPos.x <= 450 + 50)
        {
            if (pointerPos.y <= 180 + 50 && pointerPos.y >= 180 - 50) inventoryManager.RemoveItemFromQA(0);
            else if (pointerPos.y <= 60 + 50 && pointerPos.y >= 60 - 50) inventoryManager.RemoveItemFromQA(1);
            else if (pointerPos.y <= -60 + 50 && pointerPos.y >= -60 - 50) inventoryManager.RemoveItemFromQA(2);
            else if (pointerPos.y <= -180 + 50 && pointerPos.y >= -180 - 50) inventoryManager.RemoveItemFromQA(3);
            else { };

        }
    }

    /*
    public void OnPointerEnter(PointerEventData eventData)
    {
        int i = 0, j = 0;
        if (eventData.position.x == -300) j = 0;
        if (eventData.position.x == -180) j = 1;
        if (eventData.position.x == -60) j = 2;
        if (eventData.position.x == 60) j = 3;
        if (eventData.position.x == 180) j = 4;
        if (eventData.position.x == 300) j = 5;
        if (eventData.position.y == 180) i = 0;
        if (eventData.position.y == 60) i = 1;
        if (eventData.position.y == -60) i = 2;
        if (eventData.position.y == -180) i = 3;

        endIndex = i * 6 + j;
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>();
    }

    
}
