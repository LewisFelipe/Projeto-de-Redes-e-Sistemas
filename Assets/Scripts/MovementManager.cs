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

    public void Rotate(Rigidbody rb, Vector2 direction, Vector3 position)
    {
        //rb.rotate
    }
}