using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour {

    // this value will be saved for the Classroom scene to read
	public string data;

	void Awake()
    {
		DontDestroyOnLoad(this);
	}
    //added by Chris
    public float swingLevel;
}
