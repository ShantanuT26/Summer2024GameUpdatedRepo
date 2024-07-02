using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackStateData : ScriptableObject
{
    public Vector2 attackPosition;
    public float attackRadius;
    public LayerMask whatIsPlayer;

}
