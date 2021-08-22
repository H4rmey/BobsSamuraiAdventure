using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float score = 1000;
    [HideInInspector]
    public Inputs input;

    public float baseHealth         = 100;
    public float baseMoveSpeed      = 10;
    public float baseJumpHeight     = 0.4f;
    public float baseAttackForce    = 5;
    public float baseAttackRange    = 0.01f;

    [Header("Health")]
    public float currentHealth = 10;

    [Header("Jumping")]
    public float    jumpHeight      = 3;
    public float    jumpAccend      = .4f;

    [Header("Real Smooth, Cha Cha")]
    public float    smoothingAirborneX      = .4f;
    public float    smoothingGroundedX      = .1f;
    public float    smoothingWallSlidingY   = .1f;
    public float    smoothingSlidingX       = .75f;

    [Header("Movement")]
    public float    moveSpeed       = 10;

    [Header("WallJumping")]
    public float        wallSlideSpeed          = 3;
    public float        wallSlideBarDepletion   = 0.1f;
    public float        wallReleaseForce        = 0.5f;

    [Header("Attacka")]
    public float        attackRange         = 10;
    public float        attackForce         = 5;
    public float        attackContinue      = 2;

    private float       gravity;
    private float       jumpForce;

    [Header("Sliding")]
    public float        slideThreshold = 9;

    private Vector2     smoothingFactor;
    private Vector2     targetVelocity;
    private Vector2     velocitySmoothing;
    [HideInInspector]
    public Vector3      velocity;

    [HideInInspector]
    public Controller2D controller;
    [HideInInspector]
    public SurroundingCheck surrounding;

    public UnityEvent JumpTrigger;
    public UnityEvent SlashTrigger;

    public enum STATE {WALKING, AIRBORNE, IDLE, WALLSLIDING, SLIDING, SLASH };
    public STATE state = STATE.IDLE;

    public bool hitTarget;

    public HealthBar hp;

    void Awake()
    {
        controller      = GetComponent<Controller2D>();
        surrounding     = GetComponent<SurroundingCheck>();

        gravity         = -(2 * jumpHeight) / Mathf.Pow(jumpAccend, 2);

        input.pHorizontal = 1;
        state = STATE.IDLE;


        StartCoroutine("ResetAttributes");
    }

    private void Update()
    {
        score -= Time.deltaTime;
        if (score < 0)
            score = 0;


        hp.currentHealth = this.currentHealth;
        hp.maxHealth = this.baseHealth;
        hp.Run();

        if (this.currentHealth < 0)
        {
            SceneManager.LoadScene("Score");
            score = 0;
        }

        PlayerPrefs.SetFloat("score", score);

        if (this.currentHealth > this.baseHealth)
            this.currentHealth = this.baseHealth;
        if (jumpAccend > 0.6f)
            jumpAccend = 0.6f;
        if (moveSpeed > 20f)
            moveSpeed = 20f;
        if (attackForce > 50f)
            attackForce = 50f;
        if (attackRange > 0.8f)
            attackRange = 0.8f;

        jumpForce = Mathf.Abs(gravity * jumpAccend);
        /*------------------------GET THE INPUT------------------------*/
        input = controller.inputHandler.input;

        //reset set the velocity to zero. when grounded
        if (controller.collisions.below)
            velocity.y = 0;

        #region STATE SETTER
        /*------------------------SET THE STATE------------------------*/
        if (input.pHorizontal == 0)
            state = STATE.IDLE;

        if (state != STATE.SLIDING && input.pHorizontal != 0 && controller.collisions.below)
            state = STATE.WALKING;

        if (!controller.collisions.below && (controller.collisions.left || controller.collisions.right))
            state = STATE.WALLSLIDING;

        if (!controller.collisions.below && !controller.collisions.left && !controller.collisions.right)
            state = STATE.AIRBORNE;

        if (controller.collisions.below && input.pDown)
            state = STATE.SLIDING;

        if (input.pSlashPressed)
            state = STATE.SLASH;
        #endregion

        #region STATE HANDLER
        /*------------------------HANDLE ALL THE STATES------------------------*/
        if (state == STATE.SLASH)
        {
            TriggerSlash();
        }
        else if (state == STATE.IDLE)
        {
            targetVelocity.x = moveSpeed * input.pHorizontal;

            TriggerJump();
        }
        else if (state == STATE.WALKING)
        {
            targetVelocity.x = moveSpeed * input.pHorizontal;
            smoothingFactor.x = smoothingGroundedX;

            TriggerJump();
        }
        else if (state == STATE.SLIDING)
        {
            Slide();

            TriggerJump();
        }
        else if (state == STATE.WALLSLIDING)
        {
            //WallSliding();
        }
        else if (state == STATE.AIRBORNE)
        {
            targetVelocity.x = moveSpeed * input.pHorizontal;
            smoothingFactor.x = smoothingAirborneX;
        }
        #endregion

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocity.x, ref velocitySmoothing.x, smoothingFactor.x);

        /*------------------------DO THE FINAL CHECKS------------------------*/
        if (controller.collisions.above)
            velocity.y = 0;

        if (state != STATE.WALLSLIDING || input.pDown)
            velocity.y += gravity * Time.deltaTime;

        /*------------------------APPLY THAT SPEED------------------------*/
        controller.Move(velocity * Time.deltaTime);
        //Debug.Log(chargeBar.charges);
        //Debug.Log(state);
        //Debug.Log(velocity.x);
    }

    private void Jump(Vector2 aDirection)
    {        
        if (aDirection.y == 0 && aDirection.x != 0)
            velocity.x = aDirection.x;
        else if (aDirection.x == 0 && aDirection.y != 0)
            velocity.y = aDirection.y;
        else
            velocity = aDirection;

        state = STATE.AIRBORNE;
    }

    private void TriggerJump()
    {
        if (input.pJumpPressed && controller.collisions.below)
        {
            Jump(new Vector2(0, jumpForce));
            JumpTrigger.Invoke();
        }
    }

    private void Slash()
    {
        AttackHandler attack = GetComponent<AttackHandler>();

        this.state = STATE.SLASH;
    }

    void TriggerSlash()
    {
        Slash();
        SlashTrigger.Invoke();
    }

    private void Slide()
    {
        targetVelocity.x = 0;
        smoothingFactor.x = smoothingSlidingX;
        if (velocity.x > -0.5 && velocity.x < 0.5)
            velocity.x = 0;
    }

    /// <summary>
    /// DEPRICATED
    /// </summary>
    /// <param name="aEnemies"></param>
    /// <returns></returns>
    private Collider2D GetClosestEnemy(Collider2D[] aEnemies)
    {
        Collider2D bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Collider2D enemy in aEnemies)
        {
            Transform potentialTarget = enemy.transform;

            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr && potentialTarget.tag == "Enemy")
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = enemy;
            }
        }

        return bestTarget;
    }

    /// <summary>
    /// DEPRICATED
    /// </summary>
    private void oldshit()
    {
        /*------------------------GET THE INPUT------------------------*/
        input = controller.inputHandler.input;

        //reset set the velocity to zero. when grounded
        if (controller.collisions.below)
            velocity.y = 0;

        /*------------------------SET THE STATE------------------------*/
        //set state to idle if not input is present
        if (input.pHorizontal == 0)
            state = STATE.IDLE;
        else if (!input.pDown)
            state = STATE.WALKING;

        if (state != STATE.IDLE && state != STATE.AIRBORNE &&
            input.pDown && (Mathf.Abs(velocity.x) > slideThreshold))
            state = STATE.SLIDING;

        if (!controller.collisions.below && (controller.collisions.left || controller.collisions.right))
            state = STATE.WALLSLIDING;

        if (!controller.collisions.below && !controller.collisions.left && !controller.collisions.right)
            state = STATE.AIRBORNE;

        //get/set base parameters

        if (state == STATE.WALKING || state == STATE.AIRBORNE)
            targetVelocity.x = moveSpeed * input.pHorizontal;
        smoothingFactor.x = (state == STATE.AIRBORNE) ? smoothingAirborneX : smoothingGroundedX;

        /*------------------------HANDLE ALL THE STATES------------------------*/
        //TODO: needs some extra work for stopping power
        if (state == STATE.SLIDING)
        {
            targetVelocity.x = 0;
            smoothingFactor.x = smoothingSlidingX;
            if (velocity.x > -0.2 && velocity.x < 0.2)
                state = STATE.IDLE;
        }
        else if (state == STATE.WALLSLIDING)
        {
            //WallSliding();
            velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocity.y, ref velocitySmoothing.y, smoothingFactor.y);
        }

        if (state != STATE.WALLSLIDING) //DO NOT MAKE ELSE IF, WILL BREAK RELEASING FROM WALLS <:o
        {
            if (input.pJumpPressed && controller.collisions.below)
                Jump(new Vector2(0, jumpForce));

            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocity.x, ref velocitySmoothing.x, smoothingFactor.x);
        }
    }

    public IEnumerator ResetAttributes()
    {
        attackForce         = baseAttackForce;
        jumpAccend          = baseJumpHeight;
        moveSpeed           = baseMoveSpeed;
        attackRange         = baseAttackRange;

        yield return null;
    }
}
