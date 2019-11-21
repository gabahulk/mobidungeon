using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        var effect = GetComponent<CardEffect>();
        print(effect.CanUse());
        if (effect.CanUse())
        {
            effect.Use();
            this.gameObject.SetActive(false);
        }
    }
}
