using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_WindUp : MonoBehaviour {
    // Target = the location of where the object will wind up.
    // CHANGE THIS VALUE IF YOU CHANGE THE CLASSROOM'S POSITION.
    // You can change this value during runtime using the inspector instead of this script.
	public Vector3 target = new Vector3(0f, 0f, 0f);
	public float speed;

	void Update () {
        // speed at which the object will wind up
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target, step);
	}
}
