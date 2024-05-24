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
    Moving,
    MaxHeight
}

public class PendulumMovingScript : MonoBehaviour
{
    public float minimumRotationSpeed;
    public float rotationSpeed; 
    public float startingAngle;
    public float windupAngle;
    private float totalRotation = 0f;
    
    public GameObject pendulum;
    public DataManager dataManagerScript;
    private bool coroutineStarted = false;

    private PendulumState currentState = PendulumState.NotMoving;

    void Start()
    {
        totalRotation = startingAngle;
        pendulum.transform.rotation = Quaternion.Euler(startingAngle, -180, 90);
    }

    void Update()
    {   
        dataManagerScript = GameObject.Find("DataManager").GetComponent<DataManager>();
        switch (currentState)
        {
            case PendulumState.NotMoving:
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    print("y pressed");
                    currentState = PendulumState.WindingUp;
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
                    currentState = PendulumState.MaxHeight;
                }
                break;
            
            case PendulumState.MaxHeight:
                if (Input.GetKeyDown(KeyCode.H))
                {
                    print("h pressed");
                    currentState = PendulumState.Moving;
                    coroutineStarted = false;
                }
                break;

            case PendulumState.Moving:
                
                // this bool is so the coroutine only happens during the moving pendulum state and doesnt happen multiple times
                if (!coroutineStarted)
                {
                    StartCoroutine(dataManagerScript.LogPendulumInfoCoroutine());
                    StartCoroutine(dataManagerScript.LogLocationCoroutine());
                    coroutineStarted = true; 
                }
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
    //takes swinglevel from mainmenu and sets pendulumwindup to that level
    public void SetWindupAngle(float swingLevel)
    {
        float maxSwingAngle = 60f;
        float minSwingAngle = 20f;
        // Use Mathf.Lerp to interpolate between minSwingAngle and maxSwingAngle based on swingLevel
        float normalizedSwingLevel = swingLevel / 5f; // Normalize swingLevel to be in the range [0, 1]
        windupAngle = Mathf.Lerp(minSwingAngle, maxSwingAngle, normalizedSwingLevel);
    }
}
