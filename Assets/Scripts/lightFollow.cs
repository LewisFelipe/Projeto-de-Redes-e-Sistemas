using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightFollow : MonoBehaviour
{
    public Transform objectToFollow;
    public Vector3 offset;
    public float followSpeed = 10;

    public void MoveToTarget(){
        Vector3 _targetPos = objectToFollow.position + 
        objectToFollow.forward * offset.z +
        objectToFollow.right * offset.x +
        objectToFollow.up * offset.y;

        transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
    }

    private void FixedUpdate(){
        MoveToTarget();
    }
}
