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
    private Animator animator;


    /*public bool isGamepad(InputControlScheme controlScheme)
    {
        if(controlScheme.namespace == "Gamepad")
            return true;
        return false;
    }*/

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

        mManager.Move(rb, move, speed, 1);
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

        if(rb.velocity.x > 0f) //Direita
        {
            animator.SetFloat("Strafe", 1f);
        }
        else if(rb.velocity.x < 0f) //Esquerda
        {
            animator.SetFloat("Strafe", -1f);
        }
        else if(rb.velocity.z > 0f) //Cima
        {
            animator.SetFloat("Forward", 1f);
        }
        else if(rb.velocity.z < 0f) // Baixo
        {
            animator.SetFloat("Forward", -1f);
        }

        
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
        animator = GetComponent<Animator>();

        playerControls.Gameplay.Move.performed += context => move = context.ReadValue<Vector2>();
        playerControls.Gameplay.Move.canceled += context => move = Vector2.zero;

        playerControls.Gameplay.Aim.performed += context => aim = context.ReadValue<Vector2>();
        playerControls.Gameplay.Aim.canceled += context => aim = Vector2.zero;
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
