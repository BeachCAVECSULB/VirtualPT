using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFinderScript : MonoBehaviour
{
    public string targetScriptName; // The name of the script to search for

    void Start()
    {
        FindObjectsWithTargetScript();
    }
    void FindObjectsWithTargetScript()
    {
        // Find all game objects with the specified script
        MonoBehaviour[] scripts = GameObject.FindObjectsOfType<MonoBehaviour>();
        Debug.Log("Looking for this script object ");
        foreach (MonoBehaviour script in scripts)
        {
            // Check if the current script's name matches the target script name
            if (script.GetType().Name == targetScriptName)
            {
                // Print the name of the game object with the target script
                Debug.Log("Found object with " + targetScriptName + " script: " + script.gameObject.name);
            }
        }
    }
}
