using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    MovementManager mManager = new MovementManager();
    PlayerControls playerControls;
    Rigidbody rb;
    public bool usingMouse = true;
    public bool isGrounded;
    public Vector2 move;
    public Vector2 aim;
    public float speed;
    public float fallSpeed;
    private Animator animator;


    /*public bool isGamepad(InputControlScheme controlScheme)
    {
        if(controlScheme.namespace == "Gamepad")
            return true;
        return false;
    }*/

    void PlayerWalkingAnimation()
    {
        if(move == Vector2.zero)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("Strafe", 0f);
            animator.SetFloat("Forward", 0f);
        }
        else
        {
           animator.SetBool("isWalking", true); 
        }

        if(move.x > 0f) //Direita
        {
            if(Input.mousePosition.x > Screen.width / 2.0f)
            animator.SetFloat("Forward", 1f);
            else if(Input.mousePosition.x < Screen.width / 2.0f)
            animator.SetFloat("Forward", -1f);
        }
        else if(move.x < 0f) //Esquerda
        {   
            if(Input.mousePosition.x < Screen.width / 2.0f)
            animator.SetFloat("Forward", 1f);
            else if(Input.mousePosition.x > Screen.width / 2.0f)
            animator.SetFloat("Forward", -1f);      

        }
        else if(move.y > 0f) //Cima
        {
            if(Input.mousePosition.y < Screen.height / 2.0f)
            animator.SetFloat("Forward", -1f);
            else
            animator.SetFloat("Forward", 1f);
        }
        else if(move.y < 0f) // Baixo
        {
            if(Input.mousePosition.y < Screen.height / 2.0f)
            animator.SetFloat("Forward", 1f);
            else
            animator.SetFloat("Forward", -1f);
        }
    }

    void PlayerAim()
    {
        Vector3 positionToLookAt;
        if(usingMouse)
        {
            aim = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            float cameraDistance = Camera.main.transform.parent.position.y - transform.position.y;
            Vector3 position = GameObject.FindGameObjectWithTag("MouseCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(aim.x, aim.y, cameraDistance));
            positionToLookAt = new Vector3(position.x, transform.position.y, position.z);
            mManager.Rotate(transform, positionToLookAt);
            //Cursor.visible = false;
        }
        else
        {
            positionToLookAt = new Vector3(aim.x + transform.position.x, transform.position.y, aim.y + transform.position.y);
            mManager.Rotate(transform, positionToLookAt);
        }
    }

    void PlayerMove()
    {
        if(isGrounded)
        {
            mManager.Move(rb, new Vector3(move.x, rb.velocity.y, move.y), speed, 1);
            PlayerWalkingAnimation();
        }
        else
        {
            mManager.Move(rb, new Vector3((move.x/2), rb.velocity.y, (move.y/2)), speed, 1);
            //FallAnimation
        }
    }

    public void InGamePause()
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
        animator = GetComponent<Animator>();
        //feetToGround = GetComponentInchildren<BoxCollider>();

        playerControls.Gameplay.Move.performed += context => move = context.ReadValue<Vector2>();
        playerControls.Gameplay.Move.canceled += context => move = Vector2.zero;

        playerControls.Gameplay.Aim.performed += context => aim = context.ReadValue<Vector2>();
        playerControls.Gameplay.Aim.canceled += context => aim = Vector2.zero;
    }
    void FixedUpdate()
    {
        PlayerMove();

        PlayerAim();
    }
}