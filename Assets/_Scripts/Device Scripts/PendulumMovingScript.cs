using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add take starting height input from user for the pendulum
// take in user input to start the pendulum swing
// the pendulum should swing until it collides   
public enum PendulumState
{
    NotMoving,
    WindingUp,
    Moving
}

public class PendulumMovingScript : MonoBehaviour
{
    public float minimumRotationSpeed;
    public float rotationSpeed; 
    public float startingAngle;
    public float windupAngle;
    private float totalRotation = 0f;

    public GameObject pendulum;

    private PendulumState currentState = PendulumState.NotMoving;

    void Start()
    {
        totalRotation = startingAngle;
        pendulum.transform.rotation = Quaternion.Euler(startingAngle, -180, 90);
    }

    void Update()
    {   
        switch (currentState)
        {
            case PendulumState.NotMoving:
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    currentState = PendulumState.WindingUp;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    currentState = PendulumState.Moving;
                }
                break;

            case PendulumState.WindingUp:
                rotationSpeed = minimumRotationSpeed;
                totalRotation += rotationSpeed;
                pendulum.transform.rotation = Quaternion.Euler(totalRotation, -180, 90);
                // print($"pendulumangle:{pendulum.transform.rotation.eulerAngles.x} windupangle: {windupAngle}");
                if (pendulum.transform.rotation.eulerAngles.x >= windupAngle)
                {
                    print("windup complete!");
                    currentState = PendulumState.NotMoving;
                }
                break;

            case PendulumState.Moving:
                rotationSpeed = rotationSpeed + Time.deltaTime * 10f;
                totalRotation -= rotationSpeed;
                pendulum.transform.rotation = Quaternion.Euler(totalRotation, -180, 90);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("User"))
        {
            print("Collision with user");
            currentState = PendulumState.NotMoving;
        }
    }
}

// public class PendulumMovingScript : MonoBehaviour
// {
//     public float rotationSpeed = 60f; // Adjust this to control the rotation speed
//     public float startingAngle;
//     public bool isMoving = true;
//     private float totalRotation = 0f;

//     public GameObject pendulum;

//     void Start()
//     {
//         // Set the initial rotation based on the startingAngle
//         pendulum.transform.rotation = Quaternion.Euler(startingAngle, -180, 90);
//     }

//     void Update()
//     {
//         if (Input.GetMouseButtonDown(0) && !isMoving)
//         {
//             isMoving = true;
//         }

//         if (isMoving)
//         {
//             // Increment the angle based on time and rotation speed
//             totalRotation += rotationSpeed * Time.deltaTime;
//             pendulum.transform.rotation = Quaternion.Euler(startingAngle - totalRotation, -180, 90);
//         }
//     }

//     private void OnCollisionEnter(Collision collision)
//     {
//         if (collision.gameObject.CompareTag("User"))
//         {
//             print("Collision with user");
//             isMoving = false;
//         }
//     }
// }

//complex pendulumcode( without the pendulum gameobject that changes rotation. Just use the bottom handle)
// public float startingAngle;
//     public Transform jointTransform;
//     public Vector3 jointPosition;
//     void Start()
//     {
//         jointPosition = jointTransform.position;
//         transform.rotation =  Quaternion.Euler(startingAngle, -90,90);
//         float distanceFromJoint = Vector3.Distance(transform.position, jointPosition);
//         Vector3 objectDirection = transform.forward;
//         print($"objectDistance:{distanceFromJoint}");
//         Vector3 pointOnCircle = CalculatePointOnCircle(jointPosition, distanceFromJoint, -startingAngle);
//         transform.position = pointOnCircle + new Vector3(0,0.2f,0.5f);

//         HingeJoint hingeJoint = GetComponent<HingeJoint>();
//         JointLimits limits = hingeJoint.limits;
//         limits.max = startingAngle +10;
//         hingeJoint.limits = limits;
//         print($"maxangle:{hingeJoint.limits.max}");
//         // Calculate the position of a point on the circumference based on the angle
//         print($"jointposition: {jointPosition},pendulumPosition:{transform.position}, distance: {distanceFromJoint}, angle: {startingAngle}");
        
//         print($"pendulumarmspawnpoint: {pointOnCircle + new Vector3(0,0.2f,0.5f)}");
//     }

//     Vector3 CalculatePointOnCircle(Vector3 center, float radius, float angleInDegrees)
//     {
//         // Convert the angle from degrees to radians
//         float angleInRadians = Mathf.Deg2Rad * angleInDegrees;

//         // Calculate the coordinates of the point on the circumference
//         float x = center.x + radius * Mathf.Cos(angleInRadians);
//         float y = center.y + radius * Mathf.Sin(angleInRadians);
//         float z = center.z; // Assuming the circle is in the xy-plane

//         return new Vector3(x, y, z);
//     }