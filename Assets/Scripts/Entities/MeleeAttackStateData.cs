using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeleeAttackStateData : ScriptableObject
{
    public float attackRadius;
    public LayerMask whatIsPlayer;
    public float attackDamage;

}
