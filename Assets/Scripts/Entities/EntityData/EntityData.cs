using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EntityData : ScriptableObject
{
    public float wallCheckDist { get; private set; } = 1.3f;
    public float groundCheckDist { get; private set; } = 1f;
}
