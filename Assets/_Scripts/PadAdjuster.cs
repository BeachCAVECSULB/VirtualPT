using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PadAdjuster : MonoBehaviour
{
    public GameObject leftPad;
    public GameObject rightPad;
    public GameObject leftShoulder;
    public GameObject rightShoulder;
    public float offset = 0f; 

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
        }
        else if (SceneManager.GetActiveScene().name == "Classroom")
        {
            Vector3 padCenter =  new Vector3(1.307f,1.65f,-2.536f);
            leftShoulder = GameObject.Find("LShoulderRep");
            rightShoulder = GameObject.Find("RShoulderRep");
            float shoulderDistance = Vector3.Distance(leftShoulder.transform.position, rightShoulder.transform.position);

            leftPad.transform.position = padCenter -  new Vector3(shoulderDistance/2,0,0);
            rightPad.transform.position = padCenter +  new Vector3(shoulderDistance/2, 0, 0);
        }
    }
}
