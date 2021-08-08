using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateAI
{
    public WalkState walkState;
    private Transform player;
    private float range = 5.5f;
    private bool playerIsClose = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override StateAI RunCurrentStateAI()
    {
        CheckPlayerRange();
        
        if(playerIsClose == false)
        {
            return walkState;
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
