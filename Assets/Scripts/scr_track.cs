using UnityEngine;

public class scr_track : MonoBehaviour
{
    public string targetTag = "YourTagHere"; // Set this in the Inspector
    public float raycastDistance = 10f; // Max distance for the raycast
    public int horizontalRayCount = 10; // Number of rays horizontally
    public int verticalRayCount = 10; // Number of rays vertically

    public Light spotlight;

    public float radius = 1.0f; // Radius of the sphere collider
    public Color gizmoColor = Color.red; // Color of the Gizmo

    private SphereCollider sphereCollider;

    public bool isdead = false;
    void Start()
    {
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = radius;
        sphereCollider.isTrigger = true; // Set as trigger to detect collisions
    }

    void Update()
    {
        if (spotlight.enabled)
        {
            CheckForTargetInSpotlight();

        }


    }

    void CheckForTargetInSpotlight()
    {
        float halfAngle = spotlight.spotAngle * 0.5f * Mathf.Deg2Rad; // Convert to radians
        Vector3 forward = spotlight.transform.forward;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            // Generate random points within the cone using spherical coordinates
            float randomAngle = Random.Range(0f, halfAngle);
            float randomDirection = Random.Range(0f, 2f * Mathf.PI);

            // Calculate the ray direction within the cone
            Vector3 rayDirection = forward;
            rayDirection = Quaternion.Euler(
                Mathf.Sin(randomAngle) * Mathf.Cos(randomDirection) * Mathf.Rad2Deg,
                Mathf.Sin(randomAngle) * Mathf.Sin(randomDirection) * Mathf.Rad2Deg,
                0
            ) * forward;

            // Cast the ray
            if (Physics.Raycast(spotlight.transform.position, rayDirection, out RaycastHit hit, raycastDistance))
            {
                // Check if the hit object has the target tag
                if (hit.collider.CompareTag(targetTag))
                {
                    Debug.Log("Spotlight is hitting object at: " + hit.collider.gameObject.transform.position);
                    GotCaught();
                    /*this.GetComponent<Animator>().speed = 0;*/
                    // You can add additional logic here, like triggering an event
                }
            }
        }
    }


    /*void CheckForTargetInSpotlight()
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
    }*/

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(other.GetComponent<ThirdPersonController>().isMoving && !other.GetComponent<ThirdPersonController>().isCrouch)
            {
                GotCaught();
            }
        }
    }

    private void GotCaught()
    {
        if (isdead) return;
        Debug.Log("DEAD");
        GameObject.Find("par_jumpscare").GetComponent<scr_death>().Death(3);
    }
    // Optional: Visualize the rays in the Scene view
    void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(spotlight.transform.position, radius);

        if (spotlight == null) return;

        Gizmos.color = Color.yellow;
        float halfAngle = spotlight.spotAngle * 0.5f * Mathf.Deg2Rad; // Convert to radians
        Vector3 forward = spotlight.transform.forward;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            // Generate random points within the cone using spherical coordinates
            float randomAngle = Random.Range(0f, halfAngle);
            float randomDirection = Random.Range(0f, 2f * Mathf.PI);

            // Calculate the ray direction within the cone
            Vector3 rayDirection = forward;
            rayDirection = Quaternion.Euler(
                Mathf.Sin(randomAngle) * Mathf.Cos(randomDirection) * Mathf.Rad2Deg,
                Mathf.Sin(randomAngle) * Mathf.Sin(randomDirection) * Mathf.Rad2Deg,
                0
            ) * forward;

            Gizmos.DrawRay(spotlight.transform.position, rayDirection * raycastDistance);
        }
    }
}
