using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource footsteep;
    private Animator anim;
    private KeyCode stepUp, stepDown, stepLeft, stepRight;

    void Start()
    {
        anim = GetComponent<Animator>();  
        stepUp = KeyCode.W;
        stepDown = KeyCode.S;
        stepLeft = KeyCode.A;
        stepRight = KeyCode.D;
    }

    void Update()
    {
        Footsteep();
    }

    private void Footsteep()
    {
        if(Input.GetKeyDown(stepUp) || Input.GetKeyDown(stepDown) || Input.GetKeyDown(stepLeft) || Input.GetKeyDown(stepRight))
        {
            footsteep.Play();
        }
        else if(Input.GetKeyUp(stepUp) || Input.GetKeyUp(stepDown) || Input.GetKeyUp(stepLeft) || Input.GetKeyUp(stepRight))
        {
            footsteep.Stop();
        }
    }
}
