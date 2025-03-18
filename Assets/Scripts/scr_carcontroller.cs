using UnityEngine;
using TMPro;
using Unity.Cinemachine;

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
    scr_WheelControl[] wheels;
    Rigidbody rigidBody;

    public Light brk_left;
    public Light brk_right;

    public Light rev_left;
    public Light rev_right;

    private bool breaking = false;

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
            float vInput = 0;
            float hInput = 0;

            if (!GameObject.Find("obj_player").GetComponent<scr_react>().isPanic)
            {
                vInput = Input.GetAxis("Vertical");
                hInput = Input.GetAxis("Horizontal");
            }
            

            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                if (gear == 1)
                {
                    gear = -1;
                    gearind.text = "R";
                    rev_left.enabled = true;
                    rev_right.enabled = true;
                }
                else if (gear == -1)
                {
                    gear = 1;
                    gearind.text = "D";
                    rev_left.enabled = false;
                    rev_right.enabled = false;
                }
            }*/

            if(vInput < -0.5f)
            {

                    brk_left.enabled = true;
                    brk_right.enabled = true;



            }
            else
            {
                brk_left.enabled = false;
                brk_right.enabled = false;
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
                breaking = false;
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
                        wheel.WheelCollider.motorTorque = vInput * currentMotorTorque;
                    }
                wheel.WheelCollider.brakeTorque = 0;
                    
                }
            else
            {
                    // If the user is trying to go in the opposite direction
                    // apply brakes to all wheels
                    wheel.WheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
                    wheel.WheelCollider.motorTorque = 0;

                    breaking = true;
                }
           }
            GameObject.Find("FreeLook Camera").GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.Value = transform.rotation.eulerAngles.y;

            /*GameObject.Find("FreeLook Camera").GetComponent<CinemachineInputAxisController>().enabled = false;
            GameObject.Find("FreeLook Camera").GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.Value = 0f;
            GameObject.Find("FreeLook Camera").GetComponent<CinemachineOrbitalFollow>().VerticalAxis.Value = 31.5f;
            GameObject.Find("FreeLook Camera").GetComponent<CinemachineOrbitalFollow>().Orbits.Center.Radius = 9;*/


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
