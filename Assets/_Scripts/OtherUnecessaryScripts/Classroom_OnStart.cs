using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Classroom_OnStart : MonoBehaviour {
	public ObjectScript script;
	private GameObject go;
	void Start () {
        // find the Main menu from the last scene (Menu)
		go = GameObject.Find("MainMenu");
        // get the scripts data values
		script = go.GetComponent<ObjectScript>();
		Debug.Log("Recieved: " + script.data);
	}
}
