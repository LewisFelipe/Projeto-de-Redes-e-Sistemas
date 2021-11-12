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

    public void Rotate(Transform transform, Vector3 positionToLookAt) //Igual o último, mas sem a necessidade de ficar falando onde é cima
    {
        transform.LookAt(positionToLookAt, Vector3.up);
    }

    public void SmoothRotate(Transform transform, Vector3 positionToLookAt, Vector3 whereIsUp, float rotationSpeed)
    {
        Quaternion rotation = Quaternion.LookRotation(positionToLookAt, whereIsUp);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    public void SmoothRotate(Transform transform, Vector3 positionToLookAt, float rotationSpeed) //Igual o último, mas sem a necessidade de ficar falando onde é cima
    {
        Quaternion rotation = Quaternion.LookRotation(positionToLookAt, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}