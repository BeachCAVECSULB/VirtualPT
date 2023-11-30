using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    private State currentState;
    public enum State
    {
        MainMenu,
        Classroom,
        QuitApplication
    }

    public static event Action<State> OnStateChanged;

    void Start()
    {
        ChangeState(State.MainMenu);
    }

    // Change the state
    public void ChangeState(State newState)
    {
        currentState = newState;

        switch (newState)
        {
            case State.MainMenu:
                // Switch to the main menu scene
                //make a button in classroom scene that lets you go back to main menu
                // save the users data to textfile when you go back to main menu
                /*Process mProcess = new Process();
                mProcess.StartInfo.FileName = "C:/Windows/System32/notepad.exe"; //change the path
                mProcess.Start();*/
                if (SceneManager.GetActiveScene().name!="MainMenu")
                {
                    SceneManager.LoadScene("MainMenu");
                }
       
                break;
            case State.Classroom:
                // Switch to the classroom scene
                if (SceneManager.GetActiveScene().name!="Classroom")
                {
                    SceneManager.LoadScene("Classroom");
                }
                break;
            case State.QuitApplication:
                // Quit the application
                Application.Quit();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnStateChanged?.Invoke(newState);
    }

    public void OnStartButtonPressed()
    {
        ChangeState(State.Classroom);
    }
}
