using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : StateAI
{
    public bool isMyCompanion;
    public bool isInFollowRange;
    public ChaseState chaseState;
    public IdleState idleState;
    public Transform player;
    private float range = 8.5f;

    public override StateAI RunCurrentStateAI()
    {
        if(isMyCompanion && isInFollowRange)
        {
            return chaseState;
        }
        else if(Vector2.Distance(transform.position, player.position) >= range)
        {
            return idleState;
        }
        else
        {
            return this;
        }
    }
}
