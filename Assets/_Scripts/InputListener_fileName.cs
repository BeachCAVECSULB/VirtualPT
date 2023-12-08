using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputListener : MonoBehaviour {
    
	public InputField userInput;
	public ObjectScript script;
	GameObject go;

	void Start(){
        // find GameObject named "Main Menu" inside the scene
		go = GameObject.Find("MainMenu");
        // get the game object's script component
		script = go.GetComponent<ObjectScript>();
		userInput = gameObject.GetComponent<InputField>();
        // add listener whenever the user changes the input field
		userInput.onEndEdit.AddListener(SubmitName);
	}

    /**
     * This method stores whatever the user inputted into the script's data value
     */
	public void SubmitName(string arg){
		script.data = arg;
	}

}
