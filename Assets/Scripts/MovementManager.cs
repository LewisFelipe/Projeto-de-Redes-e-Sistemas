using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager
{
    //Por favor referêncie esse código em FixedUpdate
    public void Move(Rigidbody rb, Vector3 direction, float speed, int sprint)
    {
        rb.velocity = new Vector3(speed * direction.x * sprint, direction.y, speed * direction.z * sprint);
    }

    public void Rotate(Transform transform, Vector3 positionToLookAt, Vector3 whereIsUp)
    {
        transform.LookAt(positionToLookAt, whereIsUp);
    }

    public void SmoothRotate(Transform transform, Vector3 positionToLookAt, Vector3 whereIsUp, float rotationSpeed)
    {
        Quaternion rotation = Quaternion.LookRotation(positionToLookAt, whereIsUp);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}