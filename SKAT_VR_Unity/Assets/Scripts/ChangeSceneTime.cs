using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTime : MonoBehaviour {

	public string NewScene = "";
	public int TimeToChange = 0;

	// Use this for initialization
	void Start () {

		StartCoroutine ("SwitchScene");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SwitchScene() {
		float f = 0;
		while(true) {
			if(f > TimeToChange) break;
			f = f+Time.deltaTime;
			yield return null;
		}
		SceneManager.LoadSceneAsync(NewScene, LoadSceneMode.Single);
	}
}
