using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// sets up 2 inputs for going back to main menu and exiting application

public class Classroom_OnStart_ButtonManager : MonoBehaviour {
    void Update(){
        // revert back to the main menu page
        if (Input.GetKeyUp(KeyCode.M))
        { 
            Debug.Log("Loading Menu");
            SceneManager.LoadScene("Menu2"); 
            
        }
        // exit application
        if (Input.GetKeyUp(KeyCode.Escape))
        { 
            Debug.Log("Quitting Application");
            Application.Quit(); 
        }
    }
}
