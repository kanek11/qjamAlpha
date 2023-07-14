using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkSideBehavior : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //depend on the velocity, turns the sprite to the moving direction by set the rotate, without touching the scale
        float MoveDirX = animator.gameObject.GetComponent<PlayerMovement>().MovingDirection.x;
        if (MoveDirX > 0)
        {
            animator.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (MoveDirX < 0)
        {
            animator.gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
