using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene_Scene2 : MonoBehaviour

{

	void Start()
	{
		StartCoroutine ("SwitchScene");
	}

	void Update ()
	{
		
	}

	IEnumerator SwitchScene() {
		float f = 0;
		while(true) {
			if(f > 30) break;
			f = f+Time.deltaTime;
			yield return null;
		}
		SceneManager.LoadScene("Kontor", LoadSceneMode.Single);
	}

}