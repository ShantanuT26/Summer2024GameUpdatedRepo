using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WalkingStateData : ScriptableObject
{
    public float walkingSpeed { get; private set; } = 4f;
}
