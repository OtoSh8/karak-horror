using UnityEngine;

public class scr_track : MonoBehaviour
{
    public string targetTag = "YourTagHere"; // Set this in the Inspector
    public float raycastDistance = 10f; // Max distance for the raycast
    public int horizontalRayCount = 10; // Number of rays horizontally
    public int verticalRayCount = 10; // Number of rays vertically

    public Light spotlight;

    void Start()
    {
        /*spotlight = GetComponent<Light>();
        if (spotlight == null || spotlight.type != LightType.Spot)
        {
            Debug.LogError("This script requires a Spotlight component.");
            enabled = false;
        }*/
    }

    void Update()
    {
        CheckForTargetInSpotlight();
    }

    void CheckForTargetInSpotlight()
    {
        float horizontalAngleStep = spotlight.spotAngle / horizontalRayCount;
        float verticalAngleStep = spotlight.spotAngle / verticalRayCount;

        Vector3 forward = spotlight.transform.forward;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            for (int j = 0; j < verticalRayCount; j++)
            {
                // Calculate the direction of the ray within the spotlight's 3D cone
                Vector3 rayDirection = Quaternion.Euler(
                    -spotlight.spotAngle / 2 + verticalAngleStep * j, // Vertical angle
                    -spotlight.spotAngle / 2 + horizontalAngleStep * i, // Horizontal angle
                    0
                ) * forward;

                // Cast the ray
                if (Physics.Raycast(spotlight.transform.position, rayDirection, out RaycastHit hit, raycastDistance))
                {
                    // Check if the hit object has the target tag
                    if (hit.collider.CompareTag(targetTag))
                    {

                        Debug.Log("Spotlight is hitting object at: " + hit.collider.gameObject.transform.position);
                        this.GetComponent<Animator>().speed = 0;
                        // You can add additional logic here, like triggering an event
                    }
                }
            }
        }
    }

    // Optional: Visualize the rays in the Scene view
    void OnDrawGizmos()
    {
        if (spotlight == null) return;

        Gizmos.color = Color.yellow;
        float horizontalAngleStep = spotlight.spotAngle / horizontalRayCount;
        float verticalAngleStep = spotlight.spotAngle / verticalRayCount;

        Vector3 forward = spotlight.transform.forward;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            for (int j = 0; j < verticalRayCount; j++)
            {
                Vector3 rayDirection = Quaternion.Euler(
                    -spotlight.spotAngle / 2 + verticalAngleStep * j, // Vertical angle
                    -spotlight.spotAngle / 2 + horizontalAngleStep * i, // Horizontal angle
                    0
                ) * forward;

                Gizmos.DrawRay(spotlight.transform.position, rayDirection * raycastDistance);
            }
        }
    }
}