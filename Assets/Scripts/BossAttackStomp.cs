using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BossAttackStomp : StateMachineBehaviour {
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    public GameObject hitBox;
    public GameObject hitBoxCopy;
    private float waitTime1 = 1.5f;
    private float waitTime2 = 1.7f;
    private float timer1 = 0.0f;
    private float timer2 = 0.0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        timer1 = 0f;
        timer2 = 0f;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        StompAtk();

        void StompAtk() {
            timer1 += Time.deltaTime;
            timer2 += Time.deltaTime;
            if (timer1 > waitTime1) {
                hitBoxCopy = (GameObject)Instantiate(hitBox, hitBox.transform.position, hitBox.transform.rotation);
                timer1 = -3f;
            }
            if (timer2 > waitTime2) {
                timer2 = -3f;
                DestroyHitBox();
            }
        }

        void DestroyHitBox() {
            if (hitBoxCopy != null) {
                Destroy(hitBoxCopy);
            }
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }
}

