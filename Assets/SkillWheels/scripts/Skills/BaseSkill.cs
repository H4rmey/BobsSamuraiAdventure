using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;


public abstract class BaseSkill
{
    public Player player;

    public float healingFactor          = 2f;
    public float accelerateFactor       = 0.2f;
    public float jumpHeightFactor       = 0.2f;
    public float damageDealingFactor    = 2f;
    public float rangeFactor            = 2f;

    public bool trigger = false;

    public virtual void DoTheSkill(BUTTON_TYPE statType, BUTTON_TYPE actionType)
    {
        if (actionType == BUTTON_TYPE.HEALTH)
        {
            switch (statType)
            {
                case BUTTON_TYPE.HEALING:
                    Healing(healingFactor);
                    break;
                case BUTTON_TYPE.ACCEL:
                    Accelerate(accelerateFactor);
                    break;
                case BUTTON_TYPE.HEIGHT:
                    JumpHeight(jumpHeightFactor);
                    break;
                case BUTTON_TYPE.DMG:
                    DamageDone(damageDealingFactor);
                    break;
                case BUTTON_TYPE.RANGE:
                    Range(rangeFactor);
                    break;
                default:
                    break;
            }

            return;
        }

        switch (statType)
        {
            case BUTTON_TYPE.HEALING:
                DoTheHealingThing(ref player.currentHealth, this.getFactor(), this.getTrigger());
                break;
            case BUTTON_TYPE.ACCEL:
                DoTheThing(ref player.moveSpeed, player.baseMoveSpeed, this.getFactor(), this.getLerpUp(), this.getLerpDown(), this.getTrigger());
                break;
            case BUTTON_TYPE.HEIGHT:
                DoTheThing(ref player.jumpAccend, player.baseJumpHeight, this.getFactor(), this.getLerpUp(), this.getLerpDown(), this.getTrigger());
                break;
            case BUTTON_TYPE.DMG:
                DoTheThing(ref player.attackForce, player.baseAttackForce, this.getFactor(), this.getLerpUp(), this.getLerpDown(), this.getTrigger());
                break;
            case BUTTON_TYPE.RANGE:
                DoTheThing(ref player.attackRange, player.baseAttackRange, this.getFactor(), this.getLerpUp(), this.getLerpDown(), this.getTrigger());
                break;
            default:
                break;
        }
    }

    public virtual void Healing(float bonusFactor) { }
    public virtual void Accelerate(float bonusFactor) {}
    public virtual void JumpHeight(float bonusFactor) {}
    public virtual void DamageDone(float bonusFactor) {}
    public virtual void Range(float bonusFactor) {}

    public void DoTheThing(ref float currentValue, float baseValue, float bonusFactor, float lerpUP, float lerpDown, bool trigger)
    {
        if (trigger)
            currentValue = Mathf.Lerp(currentValue, baseValue * bonusFactor, lerpUP);
        else
            currentValue = Mathf.Lerp(currentValue, baseValue, lerpDown);
    }

    public void DoTheHealingThing(ref float currentValue, float bonusFactor, bool trigger)
    {
        if (trigger)
            currentValue += Time.deltaTime * bonusFactor;
    }

    public virtual bool getTrigger()
    {
        return true;
    }

    public virtual float getFactor()
    {
        return 0.0f;
    }

    public virtual float getLerpUp()
    {
        return 0.0f;
    }

    public virtual float getLerpDown()
    {
        return 0.0f;
    }
}
