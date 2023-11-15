using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classroom_OnStart_AttachConstant : MonoBehaviour {

    public GameObject head;
    
    void Start () {
        // finds the HeadNode from the VRManager
        head = GameObject.Find("HeadNode");
        // adds the consant position script to the HeadNode
        head.AddComponent(System.Type.GetType("Classroom_HeadNodePosition"));
        head.AddComponent(System.Type.GetType("Classroom_WriteData"));
    }
}
