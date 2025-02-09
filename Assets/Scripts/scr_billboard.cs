using System.Collections;
using UnityEngine;

public class scr_billboard : MonoBehaviour
{
    public Transform targetObject; // The 3D object to follow
    [SerializeField] bool FreezeZX = false;
    public Camera mainCamera;      // The main camera rendering the scene
    private RectTransform rectTransform;

    public Vector3 offset;

    void Start()
    {
        // Cache the RectTransform of the UI element
        rectTransform = GetComponent<RectTransform>();

        // Use the main camera if none is assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        /*if (FreezeZX)
        {
            transform.rotation = Quaternion.Euler(0f,Camera.main.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }*/


        if (targetObject == null || mainCamera == null) return;

        // Convert the 3D position of the target to a 2D screen position
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(targetObject.position);

        // Check if the object is in front of the camera
        if (screenPosition.z > 0)
        {
            // Update the UI position
            rectTransform.position = screenPosition+offset;
        }
        else
        {
            // Hide the icon if the object is behind the camera
            rectTransform.position = new Vector3(-1000, -1000, 0);
        }
    }

}


