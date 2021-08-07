using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateAI
{
    public WalkState walkState;
    public Transform player;
    private float range = 8f;

    public override StateAI RunCurrentStateAI()
    {
        
        if(Vector2.Distance(transform.position, player.position) <= range)
        {
            return walkState;
        }
        else
        {
            return this;
        }
    }
}
