using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public GameObject attackPosition;
    public bool usingMouse = true;
    public bool isGrounded = false, attackButtonPressed = false, usePotion = false;
    public Vector2 move;
    public int sprint;
    public Vector2 aim;
    public float speed;
    public float rotationSpeed;
    public float fallSpeed;
    public static int pauseChanged = 0;
    
    MovementManager mManager = new MovementManager();
    PlayerControls playerControls;
    Rigidbody rb;
    Animator animator;

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
           animator.SetFloat("Forward", 1f);
        }
    }

    void PlayerAim()
    {
        Vector3 positionToLookAt = Vector3.zero;
        switch (BasicAttack.isAttacking)
        {
            case true:
                if(usingMouse)
                {
                    aim = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    float cameraDistance = Camera.main.transform.parent.position.y - transform.position.y;
                    Vector3 position = GameObject.FindGameObjectWithTag("MouseCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(aim.x, aim.y, cameraDistance));
                    positionToLookAt = new Vector3(position.x, attackPosition.transform.position.y, position.z);
                    mManager.Rotate(transform, positionToLookAt, Vector3.up);
                    //Cursor.visible = false;
                }
                else
                {
                    positionToLookAt = new Vector3(aim.x + transform.position.x, attackPosition.transform.position.y, aim.y + transform.position.z);
                    mManager.Rotate(transform, positionToLookAt, Vector3.up);
                }
                break;
            default:
                if(usingMouse)
                {
                    aim = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    float cameraDistance = Camera.main.transform.parent.position.y - transform.position.y;
                    Vector3 position = GameObject.FindGameObjectWithTag("MouseCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(aim.x, aim.y, cameraDistance));
                    positionToLookAt = new Vector3(position.x, attackPosition.transform.position.y, position.z);
                    mManager.Rotate(attackPosition.transform, positionToLookAt, Vector3.up);
                    //Cursor.visible = false;
                }
                else
                {
                    positionToLookAt = new Vector3(aim.x + transform.position.x, attackPosition.transform.position.y, aim.y + transform.position.z);
                    mManager.Rotate(attackPosition.transform, positionToLookAt, Vector3.up);
                }
                break;
        }
    }

    void PlayerMove()
    {
        if(!BasicAttack.isAttacking)
        {
            mManager.SmoothRotate(transform, new Vector3(move.x, 0, move.y), Vector3.up, rotationSpeed);
            mManager.Move(rb, new Vector3(move.x, rb.velocity.y, move.y), speed, 1 << sprint);
            PlayerWalkingAnimation();
        }
        /*if(isGrounded)
        {
            mManager.Move(rb, new Vector3(move.x, rb.velocity.y, move.y), speed, 1 << sprint);
            PlayerWalkingAnimation();
        }
        else
        {
            mManager.Move(rb, new Vector3((move.x/2), rb.velocity.y, (move.y/2)), speed, 1);
            //FallAnimation
        }*/
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

        playerControls.Gameplay.Run.performed += context => sprint = 1;
        playerControls.Gameplay.Run.canceled += context => sprint = 0;

        playerControls.Gameplay.Move.performed += context => move = context.ReadValue<Vector2>();
        playerControls.Gameplay.Move.canceled += context => move = Vector2.zero;

        playerControls.Gameplay.Aim.performed += context => aim = context.ReadValue<Vector2>();

        playerControls.Gameplay.Attack.performed += context => attackButtonPressed = true;
        playerControls.Gameplay.Attack.canceled += context => attackButtonPressed = false;

        playerControls.Gameplay.UsePotion.performed += context => usePotion = true;
        playerControls.Gameplay.UsePotion.canceled += context => usePotion = false;

        playerControls.Gameplay.Pause.canceled += context => pauseChanged = ~pauseChanged;
    }

    void FixedUpdate()
    {
        switch (InGameMenu.Paused)
        {
            case true:
                break;
            default:
                PlayerMove();
                PlayerAim();
                break;
        }
    }
}