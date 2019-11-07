using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{

    public Rigidbody2D characterRigidbody;
    public float speed;
    public TouchAnalogStickController controller;
    public Animator animator;

    private Vector2 lastDeltaMovement;

    void Awake()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
}
