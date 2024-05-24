using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraAdjuster : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of camera movement
    public GameObject cameraNode;
    public float cameraAdjustmentHeight;
    public float heightMultiplier;
    public Text cameraPositionText;
    void Update()
    {
        if (SceneManager.GetActiveScene().name=="MainMenu")
        {
            cameraNode = GameObject.Find("Main Camera");
            cameraAdjustmentHeight = 0f;
            cameraPositionText = GameObject.Find("CameraPositionText").GetComponent<Text>();
        }

		else if (SceneManager.GetActiveScene().name=="Classroom")
        {
            moveSpeed = 0.5f;
            cameraNode = GameObject.Find("Main Camera");
            GameObject headNode = GameObject.Find("HeadRep");
            cameraPositionText = GameObject.Find("CameraPositionText").GetComponent<Text>();
            cameraAdjustmentHeight = headNode.transform.position.y * heightMultiplier;
        }
        
        // Get input from arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float depthInput = Input.GetAxis("Depth");
        if (Input.GetKeyDown(KeyCode.U))
        {
            heightMultiplier +=1f;
        }
                
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, depthInput).normalized;
        // print(cameraNode.transform.position);

        // Move the camera in the calculated direction
        cameraNode.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        if (cameraPositionText != null)
        {
            cameraPositionText.text = "Camera Position: " + cameraNode.transform.position.ToString() +"HeightMultiplier: " + heightMultiplier;
        }
    }
}