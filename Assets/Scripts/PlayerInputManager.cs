using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    MovementManager mManager = new MovementManager();
    PlayerControls playerControls;
    Rigidbody rb;
    public bool isGrounded;
    public Vector2 move;
    public Vector2 aim;
    public float speed;

    void PlayerAim()
    {
        mManager.Rotate(transform, aim);
    }

    void PlayerMove()
    {
        mManager.Move(rb, move, speed, 1);
    }

    public void Pause()
    {

    }

    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void OnEnable()
    {
        playerControls.Gameplay.Enable();
    }

    void OnDisable()
    {
        playerControls.Gameplay.Disable();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerControls = new PlayerControls();

        playerControls.Gameplay.Move.performed += context => move = context.ReadValue<Vector2>();
        playerControls.Gameplay.Move.canceled += context => move = Vector2.zero;

        playerControls.Gameplay.Aim.performed += context => aim = context.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        if(isGrounded)
        {
            PlayerMove();
        }

        PlayerAim();
    }
}
