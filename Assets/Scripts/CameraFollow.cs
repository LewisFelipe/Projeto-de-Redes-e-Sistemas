using UnityEngine;

public class CameraFollow : BasicFollower
{
    private void Update()
    {
        GameObject.FindGameObjectWithTag("MouseCamera").transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}