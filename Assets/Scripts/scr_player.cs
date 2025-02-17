using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using TMPro;
using Fungus;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Player Settings")]
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f; // Jump force variable
    public LayerMask groundLayer; // To check if the player is grounded
    public LayerMask carLayer;
    private bool isSprint;
    private bool isCrouch;

    [Header("Camera Settings")]
    [SerializeField] private CinemachineCamera Camfreelook;
    public Transform cameraTarget;
    public float cameraSensitivity = 2f;
    public float cameraDistance = 5f;
    public Vector2 cameraClamp = new Vector2(-40f, 80f);

    private Rigidbody rb;
    private Transform cameraTransform;
    private bool isGrounded;

    private Vector2 inputMovement;
    private Vector2 inputLook;
    private float cameraPitch;
    private float cameraYaw;

    private bool inCar;
    public GameObject objcar;
    public GameObject objint;

    public GameObject smokevol;
    public GameObject txtgear;

    public TextMeshProUGUI txtint;

    public GameObject barrierpref;
    private GameObject spawnedbar;


    public bool jump;

    // In your start method or wherever you want to change gravity
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Physics.gravity = new Vector3(0f, -40f, 0f); // Y value determines the strength of gravity
    }



    private void Awake()
    {
        inCar = false;
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        smokevol.transform.position = new Vector3(this.gameObject.transform.position.x,5,this.gameObject.transform.position.z);
        HandleInput();
        /*UpdateCamera();*/
        CheckCar();
        this.transform.GetChild(0).GetComponent<Animator>().SetBool("isGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        if (!inCar)
        {
            MovePlayer();
        }
        
        RotatePlayer();
        
    }

    private void HandleInput()
    {
        // Movement input
        float moveX = Input.GetKey(KeyCode.A) ? -1f : Input.GetKey(KeyCode.D) ? 1f : 0f;
        float moveZ = Input.GetKey(KeyCode.W) ? 1f : Input.GetKey(KeyCode.S) ? -1f : 0f;
        inputMovement = new Vector2(moveX, moveZ);
        isSprint = Input.GetKey(KeyCode.LeftShift);
        isCrouch = Input.GetKey(KeyCode.LeftControl);
        this.transform.GetChild(0).GetComponent<Animator>().SetBool("isSprint", isSprint);
        this.transform.GetChild(0).GetComponent<Animator>().SetBool("isCrouch", isCrouch);


        // Look input
        inputLook.x = Input.GetAxis("Mouse X");
        inputLook.y = Input.GetAxis("Mouse Y");
    }

    private void MovePlayer()
    {
        Vector3 move = new Vector3(inputMovement.x, 0, inputMovement.y);
        move = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0) * move;
        move.Normalize();


        this.transform.GetChild(0).GetComponent<Animator>().SetFloat("Motion",(move.x + move.y) == 0 ? 0 : Mathf.Abs(Mathf.Sign((move.x + move.y))));

        rb.MovePosition(rb.position + move * (isCrouch ? movementSpeed-1 : isSprint ? movementSpeed+3 : movementSpeed) * Time.fixedDeltaTime);
    }

    private void RotatePlayer()
    {
        Vector3 targetDirection = new Vector3(inputMovement.x, 0, inputMovement.y);
        if (targetDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private void UpdateCamera()
    {
        // Adjust pitch and yaw based on mouse input
        cameraPitch -= inputLook.y * cameraSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, cameraClamp.x, cameraClamp.y); // Clamp pitch to prevent over-rotation

        cameraYaw += inputLook.x * cameraSensitivity;

        // Rotate the camera around the target
        Quaternion rotation = Quaternion.Euler(cameraPitch, cameraYaw, 0);
        cameraTransform.position = cameraTarget.position - (rotation * Vector3.forward * cameraDistance);

        // Ensure the camera looks at the target
        cameraTransform.LookAt(cameraTarget);
    }
    
    private void OnCollisionStay(UnityEngine.Collision collision)
    {
        
        // Check if the player is touching an object with the "Ground" layer
        if (collision.gameObject.CompareTag("Ground"))
        {
           
            Jump();
            isGrounded = true;
            


        }
        else
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {

        if (isGrounded && Input.GetKey(KeyCode.Space)) // Spacebar for jumping
        {
            jump = true;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply upward force
            isGrounded = false;
            this.transform.GetChild(0).GetComponent<Animator>().SetBool("isJump", jump);
        }
        else
        {
            jump = false;
            this.transform.GetChild(0).GetComponent<Animator>().SetBool("isJump", jump);

        }
    }
    private void OnDrawGizmos()
    {
        // Set the Gizmo color (for example, blue)
        Gizmos.color = Color.blue;

        // Draw the sphere in the Scene view at the object's position
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
        private void CheckCar()
        {
        bool showint = false;

        if (!inCar)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f, carLayer);

            // If there are any colliders in the area
            if (hitColliders.Length > 0)
            {
                objint.SetActive(true);
                foreach (Collider hit in hitColliders)
                {
                    // Now you can access the collided object


                    
                    HandleInteractables(hit);

                }
            }
            else
            {
                objint.SetActive(false);
            }
        }
        else if (inCar)
        {
            objint.SetActive(false);
            this.transform.localPosition = new Vector3(2,0,0);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f, 0);

                // If there are any colliders in the area
                if (hitColliders.Length <= 0)
                {
                    txtgear.SetActive(false);
                    inCar = false;
                    this.GetComponent<MeshRenderer>().transform.GetChild(0).gameObject.SetActive(true);
                    this.transform.position = GameObject.Find("spawnpoint").transform.position;
                    this.GetComponent<Rigidbody>().useGravity = true;
                    this.gameObject.transform.parent = null;
                    objcar.GetComponent<scr_carcontroller>().incar = false;
                    this.GetComponent<CapsuleCollider>().enabled = true;
                    Camfreelook.LookAt = this.transform;
                    Camfreelook.Target.TrackingTarget = this.transform;

                    GameObject.Find("FreeLook Camera").GetComponent<CinemachineInputAxisController>().enabled = true;
                    GameObject.Find("FreeLook Camera").GetComponent<CinemachineOrbitalFollow>().Orbits.Center.Radius = 5.3f;
                    GameObject.Find("FreeLook Camera").GetComponent<CinemachineOrbitalFollow>().Orbits.Center.Height = 1.15f;
                }
                
            }
        }

    }

    private void ChangeToCar()
    {
        txtgear.SetActive(true);
        inCar = true;
        this.gameObject.transform.SetParent(objcar.transform, true);
        transform.position = objcar.transform.position;
        this.GetComponent<MeshRenderer>().transform.GetChild(0).gameObject.SetActive(false);
        objcar.GetComponent<scr_carcontroller>().incar = true;
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<Rigidbody>().useGravity = false;

        Camfreelook.LookAt = objcar.transform;
        Camfreelook.Target.TrackingTarget = objcar.transform;

        GameObject.Find("FreeLook Camera").GetComponent<CinemachineInputAxisController>().enabled = false;

        GameObject.Find("FreeLook Camera").GetComponent<CinemachineOrbitalFollow>().VerticalAxis.Value = 31.5f;
        GameObject.Find("FreeLook Camera").GetComponent<CinemachineOrbitalFollow>().Orbits.Center.Radius = 9;
          GameObject.Find("FreeLook Camera").GetComponent<CinemachineOrbitalFollow>().Orbits.Center.Height = 3;


    }

    private void HandleInteractables(Collider h)
    {


            GameObject collidedObject = h.gameObject;
        if (collidedObject.gameObject.CompareTag("Car"))
        {

            txtint.text = "Get in";
            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeToCar();
            }
        }
        else if (collidedObject.gameObject.CompareTag("Interact"))
        {
            txtint.text = "Interact";
            if (Input.GetKeyDown(KeyCode.E))
            {
                collidedObject.gameObject.GetComponent<InteractableFungusCharacter>().Interact();
            }
        }
        else if (collidedObject.gameObject.CompareTag("Item"))
        {
            txtint.text = collidedObject.name;
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (collidedObject.gameObject.name.ToLower())
                {
                    case "vape":
                    this.GetComponent<scr_inventory>().PickupItem(1);
                    Destroy(collidedObject.gameObject);
                        break;
                    case "ring":
                        this.GetComponent<scr_inventory>().PickupItem(2);
                        BooleanVariable boolVariable = GameObject.Find("model_player").GetComponent<Flowchart>().GetVariable<BooleanVariable>("hasRing");
                        boolVariable.Value = true;
                        
                        Destroy(collidedObject.gameObject);
                        break;
                    case null:
                        break;
                }
            }
        }
    }

    public void StartDialog()
    {
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("FreeLook Camera").GetComponent<CinemachineInputAxisController>().enabled = false;
    }

    public void FinishDialog()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find("FreeLook Camera").GetComponent<CinemachineInputAxisController>().enabled = true;
    }

    public void SpawnBarrier()
    {
        spawnedbar=Instantiate(barrierpref, GameObject.Find("obj_controller").GetComponent<scr_controller>().crnt_level1_obj.transform.position + new Vector3(0,20,45), Quaternion.identity);
    }

    public void DestroyBarrier()
    {
        if(spawnedbar != null)
        {
            Destroy(spawnedbar);
        }

    }


}
