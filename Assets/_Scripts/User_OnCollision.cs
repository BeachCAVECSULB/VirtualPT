using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User_OnCollision : MonoBehaviour
{
    private TactorAutoSetup tactorScript;
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Pad"))
        {
            tactorScript = GameObject.Find("TactorManager").GetComponent<TactorAutoSetup>();
            StartCoroutine(tactorScript.FireInSequence());
        }
    }
}
