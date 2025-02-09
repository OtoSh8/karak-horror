using UnityEngine;
using TMPro;
public class scr_carcontroller : MonoBehaviour
{
    public int gear = 1; //1 is drive -1 is reverse
    public float motorTorque = 2000;
    public float brakeTorque = 2000;
    public float maxSpeed = 20;
    public float steeringRange = 30;
    public float steeringRangeAtMaxSpeed = 10;
    public float centreOfGravityOffset = -1f;
    public bool incar = false;
    [SerializeField] TMP_Text gearind;

    scr_WheelControl[] wheels;
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        // Adjust center of mass vertically, to help prevent the car from rolling
        rigidBody.centerOfMass += Vector3.up * centreOfGravityOffset;

        // Find all child GameObjects that have the WheelControl script attached
        wheels = GetComponentsInChildren<scr_WheelControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (incar == true)
        {
            float vInput = Input.GetAxis("Vertical");
            float hInput = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (gear == 1)
                {
                    gear = -1;
                    gearind.text = "R";
                }
                else if (gear == -1)
                {
                    gear = 1;
                    gearind.text = "D";
                }
            }
        
        

        // Calculate current speed in relation to the forward direction of the car
        // (this returns a negative number when traveling backwards)
        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);


        // Calculate how close the car is to top speed
        // as a number from zero to one
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, forwardSpeed);

        // Use that to calculate how much torque is available 
        // (zero torque at top speed)
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);

        // �and to calculate how much to steer 
        // (the car steers more gently at top speed)
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

        // Check whether the user input is in the same direction 
        // as the car's velocity
        bool isAccelerating = Mathf.Sign(gear*vInput) == Mathf.Sign(forwardSpeed);
        
        foreach (var wheel in wheels)
        {
            // Apply steering to Wheel colliders that have "Steerable" enabled
            if (wheel.steerable)
            {
                wheel.WheelCollider.steerAngle = hInput * currentSteerRange;
            }

            if (isAccelerating)
            {
                // Apply torque to Wheel colliders that have "Motorized" enabled
                if (wheel.motorized)
                {
                    wheel.WheelCollider.motorTorque = gear * Mathf.Abs(vInput) * currentMotorTorque;
                }
                wheel.WheelCollider.brakeTorque = 0;
            }
            else
            {
                // If the user is trying to go in the opposite direction
                // apply brakes to all wheels
                wheel.WheelCollider.brakeTorque = Mathf.Abs(gear * vInput) * brakeTorque;
                wheel.WheelCollider.motorTorque = 0;
            }
           }
            

        }
        else if (incar == false)
        {
            foreach (var wheel in wheels)
            {
                wheel.WheelCollider.brakeTorque = brakeTorque;
                wheel.WheelCollider.motorTorque = 0;
            }
        }
    }

}
