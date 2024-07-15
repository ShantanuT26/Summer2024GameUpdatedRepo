using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public float runSpeed = 8f;
    public float jumpVelocity = 7f;
    public float groundCheckRadius = 0.3f;
    public LayerMask isGround;
    public int numJumps = 3;
    public float maxJumpTime = 0.4f;
}
