using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ItemData : MonoBehaviour {//, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler{

    public Item item;
    public int amount;
    public int slot;
    private Inventory inv;
    private Vector2 offset;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    /*

    public void OnPointerDown(PointerEventData eventData)
    {
        if (item != null)
        {
            offset = eventData.position - (Vector2)this.transform.position;
            Vector2 localpos = this.transform.parent.position;
            this.transform.localPosition = inv.slots[slot].transform.localPosition;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {

            this.transform.position = eventData.position - offset;
            this.transform.SetParent(this.transform.parent.parent);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.position = eventData.position - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.transform.position = inv.slots[slot].transform.position;
        //Guarantee to a center on parents position
        this.transform.SetParent(inv.slots[slot].transform);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    */
}
