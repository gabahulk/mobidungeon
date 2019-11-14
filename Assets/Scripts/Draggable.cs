using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject attackZone;
    public Transform dockTarget;


    public void OnBeginDrag(PointerEventData eventData)
    {
        attackZone.SetActive(true);
        dockTarget = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(dockTarget);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        attackZone.SetActive(false);
    }
}
