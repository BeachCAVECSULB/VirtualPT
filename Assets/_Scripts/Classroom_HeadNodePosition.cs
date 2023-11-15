using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classroom_HeadNodePosition : MonoBehaviour {

    void Update () {
        // Keeps the head node at a constant position so that
        // no matter where you are in the cave, you see only the front of the pendulum.
        // This means the user cannot move around the area.
        transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
    }
}
