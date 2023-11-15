using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classroom_OnStart : MonoBehaviour {
	public ObjectScript script;
	private GameObject go;
	void Start () {
        // find the Main men from the last scene (Menu)
		go = GameObject.Find("MainMenu");
        // get the scripts dat values
		script = go.GetComponent<ObjectScript>();
		Debug.Log("Recieved: " + script.data);
	}
}
