using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CardEffect : MonoBehaviour
{
    private GameObject player;
    public float slashForce;

    public delegate void CardEffectEndDelegate();

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    public void Use()
    {
        var target = player.GetComponent<PlayerAim>().targetEnemy;
        Vector2 direction = target.transform.position - player.transform.position;
        player.GetComponent<TopDownCharacterController>().Dash(direction, slashForce, UseEnd);
        player.GetComponent<CharacterStats>().attacking = true;
        Destroy(GetComponent<Draggable>());
        GetComponent<CanvasGroup>().alpha = 0;
    }

    public void UseEnd()
    {
        player.GetComponent<CharacterStats>().attacking = false;
        Destroy(this.gameObject);
    }

    public bool CanUse()
    {
        var target = player.GetComponent<PlayerAim>().targetEnemy;
        return target;
    }
}
