using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour

{

	void Start()
	{
		
	}

	void Update ()
	{
		
	}
	IEnumerator Example()
	{
		
	yield return new WaitForSeconds(5);
	// Only specifying the sceneName or sceneBuildIndex will load the scene with the Single mode
	SceneManager.LoadScene("Lydrejse", LoadSceneMode.Single);

	}

}