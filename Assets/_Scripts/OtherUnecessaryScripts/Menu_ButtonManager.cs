using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_ButtonManager : MonoBehaviour {

	public void StartButton(string sceneName){
		SceneManager.LoadScene(sceneName);
	}
}
