using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum_OnCollision : MonoBehaviour
{
    public AudioSource thudSound;

    void Start()
    {
        thudSound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("User"))
        {
            thudSound.Play();
        }
    }
}
