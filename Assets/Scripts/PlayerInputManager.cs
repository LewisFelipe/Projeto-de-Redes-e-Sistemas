using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    //Github test
    MovementManager mManager = new MovementManager();
    PlayerInput playerInput;
    Rigidbody rb;
    public Vector2 move;
    public float maxSpeed, speed;

    public void PlayerMove()
    {
        mManager.Move(rb, move, speed, maxSpeed, 1);
    }

    public void Pause()
    {

    }

    private void PlayerInput_onActionTriggered(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.onActionTriggered += PlayerInput_onActionTriggered;
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        //Debug.Log(move);
    }

    void FixedUpdate()
    {
        PlayerMove();
        //if(Input.)
    }
}
