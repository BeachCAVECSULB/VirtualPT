using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script is for aesthetic purposes only. This program continuously
 * turns the motor from the hinge joint on and off, allowing the joint
 * to move back and forth infintely.
 * */
public class MovingDevice_InfiniteMove : MonoBehaviour 
{

	HingeJoint joint;
	JointMotor motor;
	private bool change = false;

	// Use this for initialization
	void Start () 
	{
		joint = GetComponent<HingeJoint>();
		motor = joint.motor;

		// Set the initial rotation to a higher angle (e.g., 45 degrees)
        JointSpring spring = joint.spring;
        spring.targetPosition = -70f;
        joint.spring = spring;

		InvokeRepeating("Move", 0.7f, 1.0f);
	}

	void Move()
	{
		if(change == false)
		{
			joint.useMotor = true;
			change = true;
		} 
		else
		{
			joint.useMotor = false;
			change = false;
		}
	}
}
