using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{
    private GameObject player;
    public float slashForce;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    public void Use()
    {
        var target = player.GetComponent<PlayerAim>().targetEnemy;
        Vector2 direction = target.transform.position - player.transform.position;
        player.GetComponent<TopDownCharacterController>().Knockback(direction, slashForce,true);
        Destroy(this.gameObject);
    }

    public bool CanUse()
    {
        var target = player.GetComponent<PlayerAim>().targetEnemy;
        return target;
    }
}
