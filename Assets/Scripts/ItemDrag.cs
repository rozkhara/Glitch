using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    Vector3 initPos;
    Vector3 endPos;
    int initIndex;
    int endIndex;
    RectTransform rt;
    Inventory inventoryManager;
    public void OnBeginDrag(PointerEventData eventData)
    {
        initPos = transform.position;
        initIndex = transform.GetSiblingIndex();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
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
            endIndex = i * 6 + j;
        }
        transform.position = initPos;
        if (initIndex != endIndex)
        {
            inventoryManager.SwapItem(initIndex, endIndex);
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

    // Update is called once per frame
    void Update()
    {

    }
}
