using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InputListner_Slider : MonoBehaviour {

    public Slider slider;
    public ObjectScript script;
    GameObject go;

    void Start()
    {
        // find GameObject named "Main Menu" inside the scene
        go = GameObject.Find("MainMenu");
        // get the game object's script component
        script = go.GetComponent<ObjectScript>();
        slider = gameObject.GetComponent<Slider>();
        // add listener whenever the user changes the slider value
        slider.onValueChanged.AddListener(delegate { SaveValue(); });
        //starting default position
        script.swingLevel = 4;
    }

    public void SaveValue()
    {
        // save to the main menu script's object
        script.swingLevel = slider.value;
    }
}
