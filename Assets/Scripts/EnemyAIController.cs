using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour, ICharacterController
{
    public float Horizontal { get; set; }
    public float Vertical { get; set; }
    public float visionRadius;
    public float knockbackForce;
    public Animator AIController;
    public Rigidbody2D enemyRigidbody;
    public SpriteRenderer sprite;
    public GameObject enemy;
    public EnemyStats stats;

    public int life = 3;

    GameObject player;
    bool isKnockbacking = false;
    // Update is called once per frame
    void Update()
    {
        CheckSurroundings();
    }

    private void CheckSurroundings()
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();

        if (Physics2D.CircleCast(transform.position, visionRadius,Vector2.zero, filter, hits) > 0)
        {
            player = GetPlayer(hits);

            if (player != null)
            {
                AIController.SetBool("isEnemyInRange", true);
            }
            else
            {
                AIController.SetBool("isEnemyInRange", false);
            }
        }
        else
        {
            AIController.SetBool("isEnemyInRange", false);
        }
    }

    private GameObject GetPlayer(List<RaycastHit2D> hits)
    {
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                return hit.collider.gameObject;
            }
        }

        return null;
    }

    public void StartPatrol()
    {

    }

    public void StopPatrol()
    {

    }

    public void StartFollow()
    {
        
    }

    public void FollowUpdate()
    {

        if (player == null)
        {
            return;
        }

        Vector2 direction = player.transform.position - this.transform.position;
        direction.Normalize();
        Horizontal = direction.x;
        Vertical = direction.y;
    }

    public void StopFollow()
    {
        Horizontal = 0;
        Vertical = 0;
        player = null;
    }

    IEnumerator DamageCoroutine()
    {
        int numberOfTimesToBlink = 3;
        while (numberOfTimesToBlink > 0)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            numberOfTimesToBlink--;
        }
    }

    private void OnPlayerCollision(GameObject player)
    {
        Vector2 direction = player.transform.position - this.transform.position;
        if (!player.GetComponent<CharacterStats>().attacking)
        {
            player.GetComponent<TopDownCharacterController>().Knockback(direction, knockbackForce);
            player.GetComponent<CharacterStats>().ReceiveDamage(stats.baseDamage);
        }
        else
        {
            StartCoroutine("DamageCoroutine");
            enemy.GetComponent<TopDownCharacterController>().Knockback(Vector2.Perpendicular(direction.normalized), 3);
            life--;

            if (life == 0)
            {
                Destroy(this.transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            OnPlayerCollision(collider.gameObject);
        }
    }
    
}
