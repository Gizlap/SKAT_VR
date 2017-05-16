using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour

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
			if(f > 98) break;
			f = f+Time.deltaTime;
			yield return null;
		}
		SceneManager.LoadScene("Lydrejse", LoadSceneMode.Single);
	}

}