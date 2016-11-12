using UnityEngine;
using System.Collections;

public class FollowCharacter : MonoBehaviour
{
    public Transform Character;
    public BoxCollider2D Bounds;
    // On what percentage of screen space should the camera start moving
    public float MoveTholdX;
    public float Speed;

    private Coroutine cameraMoveCoroutine;

    // Use this for initialization
    void Start()
    {
        Camera.main.transform.position = GetCenterCamPos();
    }

    // Update is called once per frame
    void Update()
    {
        // Camera movement only on X axis
        // We want to move the camera only if the character moves close to the edge
        Bounds camBounds = Camera.main.OrthographicBounds();
        float tholdDistanceX = (camBounds.max.x - camBounds.min.x) * MoveTholdX; // How far are the illusionary edges from screen edge?
        // Calculate the actual on screen x coordinates
        float minTholdX = camBounds.min.x + tholdDistanceX;
        float maxTholdX = camBounds.max.x - tholdDistanceX;

        // Move the camera only when too close to the edge
        if (Character.position.x < minTholdX ||
            Character.position.x > maxTholdX)
        {
            if (cameraMoveCoroutine != null)
                StopCoroutine(cameraMoveCoroutine);
            cameraMoveCoroutine = StartCoroutine(MoveCameraTo(GetCenterCamPos()));
        }
    }

    // Smooth camera movement to target position
    private IEnumerator MoveCameraTo(Vector3 target)
    {
        while (Vector3.Distance(Camera.main.transform.position, target) > 0.05f)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, target, Speed * Time.deltaTime);
            yield return null;
        }
    }

    // Returns camera position aligned to player center but camera bounds cannot exceed Bounds
    private Vector3 GetCenterCamPos()
    {
        // Center the camera to the character position
        Vector3 adjustedPos = Character.position;
        
        // No reason to move the camera on the z axis
        adjustedPos.z = Camera.main.transform.position.z;

        Bounds cameraBounds = Camera.main.OrthographicBounds();
        float xDist = adjustedPos.x - Camera.main.transform.position.x;
        float yDist = adjustedPos.y - Camera.main.transform.position.y;

        // Adjust the bounds to see how they would be if we moved the camera to character position
        cameraBounds.min = new Vector3(cameraBounds.min.x + xDist, cameraBounds.min.y + yDist);
        cameraBounds.max = new Vector3(cameraBounds.max.x + xDist, cameraBounds.max.y + yDist);

        // Left right cropping
        if (cameraBounds.min.x < Bounds.bounds.min.x)
            adjustedPos.x += Bounds.bounds.min.x - cameraBounds.min.x;
        else if (cameraBounds.max.x > Bounds.bounds.max.x)
            adjustedPos.x -= cameraBounds.max.x - Bounds.bounds.max.x;

        // Top bottom cropping
        if (cameraBounds.min.y < Bounds.bounds.min.y)
            adjustedPos.y += Bounds.bounds.min.y - cameraBounds.min.y;
        else if (cameraBounds.max.y > Bounds.bounds.max.y)
            adjustedPos.y -= cameraBounds.max.y - Bounds.bounds.max.y;

        return adjustedPos;
    }
}
