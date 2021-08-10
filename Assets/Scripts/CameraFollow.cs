using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target, camera;
    public bool shouldSmooth, shouldLookAtPlayer;
    public float smoothSpeed = 0.125f;
    public Vector2 velocity;
    public Vector3 offset = new Vector3();

    private void Follow()
    {
        switch(shouldSmooth)
        {
            case true:
                float x = Mathf.SmoothDamp(transform.position.x, target.transform.position.x + offset.x, ref velocity.x, smoothSpeed);
                float z = Mathf.SmoothDamp(transform.position.z, target.transform.position.z + offset.z, ref velocity.y, smoothSpeed);
                transform.position = new Vector3(x, target.position.y + offset.y, z);
            break;
            default:
                transform.position = target.position + offset;
            break;
        }
        switch(shouldLookAtPlayer)
        {
            case true:
                camera.transform.LookAt(target);
            break;
            default:
            break;
        }
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        camera = Camera.main.transform;
    }

    private void Update()
    {
        GameObject.FindGameObjectWithTag("MouseCamera").transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void LateUpdate()
    {
        switch(shouldSmooth)
        {
            case false:
                Follow();
            break;
            default:
            break;
        }
    }

    private void FixedUpdate()
    {
        switch(shouldSmooth)
        {
            case true:
                Follow();
            break;
            default:
            break;
        }
    }

}
