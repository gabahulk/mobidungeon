using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public GameObject aim;
    public GameObject targetEnemy;
    public CharacterStats stats;
    private ContactFilter2D filter;

    private void Start()
    {
        filter = new ContactFilter2D();
        filter.layerMask = LayerMask.GetMask("Enemy");
        filter.useLayerMask = true;
        filter.useTriggers = true;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, stats.visionRadius, LayerMask.GetMask("Enemy"));
        if (colliders.Length > 0)
        {
            aim.SetActive(true);
            aim.transform.position = colliders[0].transform.position;
            targetEnemy = aim.gameObject;
        }
        else
        {
            targetEnemy = null;
            aim.SetActive(false);
        }
    }


    
}
