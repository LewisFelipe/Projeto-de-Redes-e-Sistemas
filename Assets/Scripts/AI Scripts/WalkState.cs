using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : StateAI
{
    public bool isMyCompanion;
    public bool isInFollowRange;
    public ChaseState chaseState;
    public IdleState idleState;
    private Transform player;
    private float range = 5.5f;
    private bool playerIsClose = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override StateAI RunCurrentStateAI()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        CheckPlayerRange();

        if(isMyCompanion && isInFollowRange)
        {
            return chaseState;
        }
        else if (playerIsClose)    
        {
            return idleState;
        }
        else
        {
            return this;
        }
    }


    private void CheckPlayerRange()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, range);
        foreach(Collider player in objects)
        {
            if(player.tag == "Player")
            {
                playerIsClose = true;
            }
            else
            {
                playerIsClose = false;
            }
        }
    }
    
}
