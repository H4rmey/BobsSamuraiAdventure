using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BossIdleBehaviour : StateMachineBehaviour {
    public float randomF;
    public int nextAtk;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        randomF = Random.Range(0f, 3f);

            if (randomF <= 1) {
                nextAtk = 1;
            }
            else if (randomF <= 2) {
                nextAtk = 2;
            }
            else if (randomF <= 3) {
                nextAtk = 3;
            }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (nextAtk == 1) {
            animator.SetTrigger("AttackLow");
        }
        else if (nextAtk == 2) {
            animator.SetTrigger("AttackStomp");
        }
        else if (nextAtk == 3) {
            animator.SetTrigger("AttackRain");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("AttackLow");
        animator.ResetTrigger("AttackStomp");
        animator.ResetTrigger("AttackRain");
    }
}