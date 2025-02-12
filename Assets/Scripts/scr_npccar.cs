using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
public class scr_npccar : MonoBehaviour
{
    [SerializeField] Transform objplayer;
    public float horizontalrange = 0f;
    public float verticalrange = 20f;
    private float vInput = 1f;
    private float hInput = 0f;
    private int straightangle = 0;
    private bool outofrange = false;
    private bool Moving = false;

    public int gear = 1; //1 is drive -1 is reverse
    public float motorTorque = 2000;
    public float brakeTorque = 2000;
    public float maxSpeed = 20;
    public float steeringRange = 30;
    public float steeringRangeAtMaxSpeed = 10;
    public float centreOfGravityOffset = -1f;

    scr_WheelControl[] wheels;
    Rigidbody rigidBody;
    public bool Collided = false;
    
    private void NPCAlgo()
    {

        /*//Horizontal
        if(objplayer.position.x > (this.transform.position.x + horizontalrange))
        {
            //Player on my right
            hInput = 1f;
            outofrange = true;
            Debug.Log("RIGHT");

        }
        else if (objplayer.position.x < (this.transform.position.x - horizontalrange))
        {
            //Player on my left
            hInput = -1f;
            outofrange = true;
            Debug.Log("LEFT");

        }
        else
        {
            hInput = 0f;
            outofrange = false;
            Debug.Log("MID");

        }

        //Vertical
        Debug.DrawLine(this.transform.position + new Vector3(-10, 0, -verticalrange), this.transform.position + new Vector3(+10, 0, -verticalrange));
        if(objplayer.position.z > this.transform.position.z-verticalrange)
        {
            //Player is closing up on me!!!!!!!!!!!!
            vInput = 1f;
            Moving = true;
        }
        else if(objplayer.position.z <= this.transform.position.z - verticalrange)
        {
            //Player is not close bro
            vInput = 0f;
            Moving = false;
        }
*/

        // Calculate the target position for the right lane
        float roadCenterX = 0;
        float laneOffset = 2;
            float targetX = roadCenterX + laneOffset;

            // Adjust horizontalInt to keep the car in the right lane
            if (transform.position.x < targetX - 0.2f)
            {
                hInput = 1; // Steer right
            }
            else if (transform.position.x > targetX + 0.2f)
            {
            hInput = -1; // Steer left
            }
            else
            {
            hInput = 0; // Go straight
            }

            // Check the distance of the following car
            if (this != null)
            {
            if (objplayer.position.z > this.transform.position.z + verticalrange)
            {
                //Player is INFRONT OF ME NOW, VANISH HEHE
                Debug.Log("Player is INFRONT OF ME NOW, VANISH HEHE");
                Destroy(gameObject);
            }
            else if (objplayer.position.z > this.transform.position.z - verticalrange)
            {
                //Player is closing up on me!!!!!!!!!!!!
                /*vInput = 1f;*/
                maxSpeed = 100;
                Moving = true;
            }
            else if (objplayer.position.z > this.transform.position.z - verticalrange*4)
            {
                //Player is not close bro
                /*vInput = 0f;*/
                maxSpeed = 20;
                Moving = true;
            }
            else if (objplayer.position.z <= this.transform.position.z - verticalrange*4)
            {
                //Player is not close bro
                /*vInput = 0f;*/
                Moving = false;
            }
        }
        

    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        // Adjust center of mass vertically, to help prevent the car from rolling
        rigidBody.centerOfMass += Vector3.up * centreOfGravityOffset;

        // Find all child GameObjects that have the WheelControl script attached
        wheels = GetComponentsInChildren<scr_WheelControl>();

        objplayer = GameObject.FindGameObjectWithTag("Player").transform;

        if (GameObject.FindGameObjectsWithTag("NPCCar").Length > 2)
        {
            Destroy(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

        NPCAlgo();

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


            if (isAccelerating && Moving)
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

    public void UnColl()
    {
        Collided = false;
    }

    public void OnColl()
    {
        Collided = true;
        StartCoroutine(CHeck());
    }

    IEnumerator CHeck()
    {
        yield return new WaitForSeconds(10f);

        if (Collided)
        {
            GameObject.Find("obj_controller").GetComponent<scr_controller>().isEscaped = true;
            Destroy(gameObject);
            Debug.Log("LEVEL 2 DONE");
        }

        yield return null;
    }

}
