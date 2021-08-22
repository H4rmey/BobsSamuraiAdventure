using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class AirtimeSkill : BaseSkill
{
    public override bool getTrigger()
    {
        return (player.state == STATE.AIRBORNE);
    }

    public override float getFactor()
    {
        return 2f;
    }

    public override float getLerpUp()
    {
        return 0.01f;
    }

    public override float getLerpDown()
    {
        return 0.01f;
    }
}
