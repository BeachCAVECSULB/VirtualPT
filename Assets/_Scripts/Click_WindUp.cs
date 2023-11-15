using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_WindUp : MonoBehaviour {

	Animator anim;
	int isWound;

	// Use this for initialization
	void Start () {

		anim = gameObject.GetComponent<Animator>();
		isWound = 1;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			if(isWound == 0){
//				anim.SetTrigger("Deactive");
//				isWound = 1;
			}
			else if(isWound == 1){
				anim.SetTrigger("Active");
				isWound = 0;
			}
				

		}
		
	}
}
