using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class DamageDealtSkill : BaseSkill
{
    public override bool getTrigger()
    {
        return (player.hitTarget && player.input.pSlashPressed);
    }

    public override float getFactor()
    {
        return 100f;
    }

    public override float getLerpUp()
    {
        return 10000f;
    }

    public override float getLerpDown()
    {
        return 0.01f;
    }
}
