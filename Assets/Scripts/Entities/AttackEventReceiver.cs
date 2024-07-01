using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEventReceiver
{
    public AttackState attackState;
    public void DoDamage()
    {
        attackState.DoDamage();
    }
    public void FinishAttack()
    {
        attackState.FinishAttack();
    }
}
