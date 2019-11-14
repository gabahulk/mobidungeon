using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public GameObject aim;
    public GameObject targetEnemy;
    public PlayerStats stats;
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
        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        if (Physics2D.CircleCast(transform.position, stats.visionRadius, Vector2.zero, filter, hits) > 0)
        {
            aim.SetActive(true);
            hits.OrderBy(a => (Vector2.Distance(transform.position, a.transform.position)));
            aim.transform.position = hits[0].transform.position;
            targetEnemy = aim.gameObject;
        }
        else
        {
            targetEnemy = null;
            aim.SetActive(false);
        }
    }


    
}
