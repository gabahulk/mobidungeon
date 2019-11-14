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

    IEnumerator KnockbackWait(float seconds)
    {
        isKnockbacking = true;
        yield return new WaitForSeconds(seconds);
        isKnockbacking = false;
        enemyRigidbody.velocity = Vector2.zero;
    }


    public void Knockback(Vector2 direction, float force)
    {
        enemyRigidbody.AddForce(direction * force, ForceMode2D.Impulse);
        StartCoroutine("KnockbackWait", 0.5f);
    }

    private void OnPlayerCollision(GameObject player)
    {
        Vector2 direction = player.transform.position - this.transform.position;
        if (!player.GetComponent<PlayerStats>().attacking)
        {
            player.GetComponent<TopDownCharacterController>().Knockback(direction, knockbackForce);
        }
        else
        {
            Knockback(-direction, 20);
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
