using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame ()
    {
        SceneManager.LoadScene("MainScene");
        /*Process mProcess = new Process();
         mProcess.StartInfo.FileName = "C:/Windows/System32/notepad.exe"; //change the path
         mProcess.Start();*/
    }
}
