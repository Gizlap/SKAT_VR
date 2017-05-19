using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour {

	public GameObject[] sounds;

	// Use this for initialization
	void Start () {
		
	}

	public void beginSounds(){
		foreach(GameObject s in sounds){
			s.SetActive(true);
		}
	}

	public void EndSounds(){
		foreach(GameObject s in sounds){
			s.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
