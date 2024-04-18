using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_MoveController : MonoBehaviour {

	public Transform startPosition;
	public Object_WindUp windUp;
    private bool isWound;
    public float speed=0.50F;
    public Rigidbody rigid;
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;
    void Start()
    {
        windUp.enabled = false;
        isWound = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) && isWound == false)
        { // wind up
            transform.position = startPosition.position;
            windUp.enabled = true;
            isWound = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) && isWound == true)
        { // release 
          // Move BoxCollider to a location that cannot disturb the Pendulum 
            transform.position = new Vector3(-10f, -10f, -10f);
            windUp.enabled = false;
            isWound = false;
        }

    } 
}
/*void Start () {
		windUp.enabled = false;
        isWound = false;
        
         startTime = Time.time;
       //  Calculate the journey length.
        journeyLength = Vector3.Distance(startPosition.position, windUp.transform.position);
	}
	
	void Update () {
		if(Input.GetKeyUp(KeyCode.RightArrow) && isWound == false){ // wind up
            
            //float distCovered = (Time.time - startTime) * speed;
            //float fractionOfJourney = distCovered / journeyLength;
            //transform.position = Vector3.Lerp(windUp.transform.position, startPosition.position, fractionOfJourney);
           // while(windUp.transform.position!=startPosition.position){
             //   float step = speed * Time.deltaTime;
			//transform.position = Vector3.MoveTowards(windUp.transform.position, startPosition.position, step);//startPosition.position;
            //}
            
			windUp.enabled = true;
            isWound = true;
		}
		else if(Input.GetKeyUp(KeyCode.LeftArrow) && isWound == true){ // release 
			// Move BoxCollider to a location that cannot disturb the Pendulum 
			transform.position = new Vector3(1.426f, 1.296f, -2.682f);
			windUp.enabled = false;
            isWound = false;
		}
	}*/
