using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class HealthSkill : BaseSkill
{
    public override void Healing(float bonusFactor)
    {
        player.currentHealth += Time.deltaTime * player.baseHealth / player.currentHealth * bonusFactor;
    }

    public override void Accelerate(float bonusFactor)
    {
        player.moveSpeed = player.baseMoveSpeed * (player.baseHealth / player.currentHealth) * bonusFactor;
    }

    public override void JumpHeight(float bonusFactor)
    {
        player.jumpAccend = player.baseJumpHeight * (player.baseHealth / player.currentHealth) * bonusFactor;
    }

    public override void DamageDone(float bonusFactor)
    {
        player.attackForce = player.baseAttackForce * (player.baseHealth / player.currentHealth) * bonusFactor;
    }

    public override void Range(float bonusFactor)
    {
        player.attackRange = player.baseAttackRange * (player.baseHealth / player.currentHealth) * bonusFactor;
    }
}
