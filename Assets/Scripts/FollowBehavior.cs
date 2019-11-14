using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehavior : StateMachineBehaviour
{
    private EnemyAIController controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.gameObject.GetComponent<EnemyAIController>();
        controller.StartFollow();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.StopFollow();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.FollowUpdate();
    }

    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}
}
