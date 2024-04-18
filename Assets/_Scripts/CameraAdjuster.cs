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



// public class CameraAdjuster : MonoBehaviour
// {

//     private int speedUp = -1;
//     public float cameraSpeed;
//     public GameObject cameraNode;

//     // Update is called once per frame
//     void Update()
//     {
//         print(transform.position);
//         if (SceneManager.GetActiveScene().name=="MainMenu")
//         {
//             cameraNode = GameObject.Find("VRManagerCenterNode");
//         }
// 		else if (SceneManager.GetActiveScene().name=="Classroom")
//         {
//             cameraNode = GameObject.Find("VRManagerCenterNode");
//         }
//         if (speedUp == -1)
//         {
//             if (Input.GetButtonDown("MoveCamera Y (Keyboard)"))
//             {
//                 if (Input.GetAxisRaw("MoveCamera Y (Keyboard)") > 0)
//                     CameraUp(cameraSpeed);
//                 else if (Input.GetAxisRaw("MoveCamera Y (Keyboard)") < 0)
//                     CameraDown(cameraSpeed);

//                 speedUp = 0;
//             }
//         }
//         else if (speedUp == 0)
//         {
//             if (Input.GetButton("MoveCamera Y (Keyboard)"))
//                 speedUp = 1;
//             else if (Input.GetButtonUp("MoveCamera Y (Keyboard)"))
//                 speedUp = -1;
//             else
//                 speedUp = -1;
//         }

//         if (speedUp == 1)
//         {
//             if (Input.GetButtonUp("MoveCamera Y (Keyboard)"))
//                 speedUp = -1;
//             else if (Input.GetAxisRaw("MoveCamera Y (Keyboard)") > 0)
//                 CameraUp(0.5f);
//             else if (Input.GetAxisRaw("MoveCamera Y (Keyboard)") < 0)
//                 CameraDown(0.5f);
//             else
//                 speedUp = -1;
//         }
//     }

//     public void CameraUp(float m)
//     {
//         transform.position = new Vector3(transform.position.x, transform.position.y + (m * Time.deltaTime), transform.position.z);
//     }

//     public void CameraDown(float m)
//     {
//         transform.position = new Vector3(transform.position.x, transform.position.y - (m * Time.deltaTime), transform.position.z);
//     }
// }

