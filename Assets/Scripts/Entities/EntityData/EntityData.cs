using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class EntityData : ScriptableObject
{
    public float wallCheckDist { get; private set; } = 1.3f;
    public float groundCheckDist { get; private set; } = 1f;

    public float meleeAttackDist = 1.6f;

    public float attackRadius;

    public float health;

    public float playerDetectedMinDist;

    public float playerDetectedMaxDist;

    public int stunResistance;

    public float stunRecovery;

    public Vector2 knockBackForce;
}
