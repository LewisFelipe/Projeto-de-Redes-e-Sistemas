using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager
{
    //Por favor referêncie esse código em FixedUpdate
    public void Move(Rigidbody rb, Vector2 direction, float speed, float maxSpeed, int sprint)
    {
        if(rb.velocity.magnitude <= maxSpeed * sprint)
        {
            Vector3 toWhere = new Vector3(direction.x, 0, direction.y);
            rb.AddForce(toWhere * speed, ForceMode.Force);
        }
    }
}