using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IdleStateData : ScriptableObject
{
    public float maxIdleTime { get; private set; } = 2f;
}
