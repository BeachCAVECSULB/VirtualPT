using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Classroom_OnStart_ButtonManager : MonoBehaviour {
    void Update(){
        // revert back to the main menu page
        if (Input.GetKeyUp(KeyCode.M)){ SceneManager.LoadScene("Menu2"); }
        // exit application
        if (Input.GetKeyUp(KeyCode.Escape)){ Application.Quit(); }
    }
}
