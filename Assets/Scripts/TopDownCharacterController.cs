using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    public Rigidbody2D characterRigidbody;
    public float speed;
    public ICharacterController controller;
    public GameObject controllerGameObject;
    public Animator animator;
    public CharacterStats playerStats;

    private Vector2 lastDeltaMovement;
    private bool isControllerLocked = false;
    private GameObject target;

    void Awake()
    {
        //Unity can't pass an interface to the inspector, so I had to do this stupid bad code.
        controller = controllerGameObject.GetComponent<ICharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isControllerLocked)
        {
            return;
        }

        Vector2 deltaMovement;

        deltaMovement.x = controller.Horizontal;
        deltaMovement.y = controller.Vertical;
        if (deltaMovement != Vector2.zero)
        {
            MoveCharacter(deltaMovement, this.transform.position);
            if (shouldFlipCharacter(lastDeltaMovement, deltaMovement))
            {
                Vector2 newScale = transform.localScale;
                newScale.x = newScale.x * - 1;
                transform.localScale = newScale;
            }
            lastDeltaMovement = deltaMovement;
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void MoveCharacter(Vector2 movement, Vector2 currentPosition)
    {
        characterRigidbody.MovePosition(currentPosition + movement * speed * Time.deltaTime);
    }

    bool shouldFlipCharacter(Vector2 lastDeltaMovement, Vector2 currentDeltaMovement)
    {
        if (Mathf.Sign(lastDeltaMovement.x)!= Mathf.Sign(currentDeltaMovement.x))
        {
            return true;
        }

        return false;
    }

    IEnumerator KnockbackWait(float seconds)
    {
        isControllerLocked = true;
        yield return new WaitForSeconds(seconds);
        isControllerLocked = false;
        characterRigidbody.velocity = Vector2.zero;
    }


    public void Knockback(Vector2 direction, float force)
    {
        characterRigidbody.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        StartCoroutine(KnockbackWait(0.5f));
    }

    IEnumerator DashWait(float seconds, CardEffect.CardEffectEndDelegate endEffect)
    {
        isControllerLocked = true;
        yield return new WaitForSeconds(seconds);
        isControllerLocked = false;
        characterRigidbody.velocity = Vector2.zero;
        endEffect();
    }

    public void Dash(Vector2 direction, float speed, CardEffect.CardEffectEndDelegate endEffect)
    {
        characterRigidbody.velocity = (direction.normalized * speed);
        StartCoroutine(DashWait(0.5f, endEffect));
    }
}
