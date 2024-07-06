using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeleeAttackStateData : ScriptableObject
{
    public float attackRadius; //NOTE: This attack radius is the ACTUAL attack radius. The one in EntityData just controls the radius of the Gizmo
    public LayerMask whatIsPlayer;
    public float attackDamage;

}
