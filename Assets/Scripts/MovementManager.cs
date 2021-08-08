using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager
{
    //Por favor referêncie esse código em FixedUpdate
    public void Move(Rigidbody rb, Vector2 direction, float speed, int sprint)
    {
        rb.velocity = new Vector3(speed * direction.x * sprint, 0, speed * direction.y * sprint);
    }

    //Essa bosta n funfa
    public void GamepadRotate(Transform transform, Vector2 direction)
    {
        //var posToScreen = Camera.main.WorldToScreenPoint(transform.position);
        //var nigger = Camera.main.ScreenToWorldPoint(new Vector3(direction.x, posToScreen.y, direction.y));
        //transform.LookAt(new Vector3(nigger.x, transform.position.y, nigger.z));
        transform.LookAt(new Vector3(direction.x, transform.position.y, direction.y) + new Vector3(transform.position.x, 0, transform.position.z));
    }
}