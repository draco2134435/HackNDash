using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public float cameraAcceleration;
    public float xOffset;
    public float yOffset;

    public float minX = -Mathf.Infinity;
    public float maxX = Mathf.Infinity;
    public float minY = -Mathf.Infinity;
    public float maxY = Mathf.Infinity;

    float zPosition;
    Vector3 currentVel;

    void Start()
    {
        target = transform.parent;
        transform.parent = null;
        //GetComponent<Camera>().transparencySortMode = TransparencySortMode.Orthographic;
        zPosition = transform.position.z;
    }

    void Update()
    {
        updateCameraPosition();
    }

    void updateCameraPosition()
    {
        Vector3 goalPosition = Vector3.forward * zPosition;
        goalPosition += Vector3.up * (target.position.y + yOffset);
        goalPosition += Vector3.right * (target.position.x + xOffset);
        goalPosition = adjustGoalPosition(goalPosition);
        transform.position = Vector3.SmoothDamp(transform.position, goalPosition, ref currentVel, cameraAcceleration);
    }

    Vector3 adjustGoalPosition(Vector3 oldPosition)
    {
        Vector3 newPosition = oldPosition;
        if (newPosition.x > maxX)
        {
            newPosition = new Vector3(maxX, newPosition.y, newPosition.z);
        }
        else if(newPosition.x < minX)
        {
            newPosition = new Vector3(minX, newPosition.y, newPosition.z);
        }

        if (newPosition.y > maxY)
        {
            newPosition = new Vector3(newPosition.x, maxY, newPosition.z);
        }
        else if(newPosition.y < minY)
        {
            newPosition = new Vector3(newPosition.x, minY, newPosition.z);
        }
        return newPosition;
    }

    public void setTarget(Transform target)
    {
        this.target = target;
    }
}
