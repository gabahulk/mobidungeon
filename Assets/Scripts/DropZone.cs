using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject card = eventData.pointerDrag.gameObject;
        if (card)
        {
            var effect = card.GetComponent<CardEffect>();
            if (effect.CanUse())
            {
                effect.Use();
                this.gameObject.SetActive(false);
            }
        }
    }

}
