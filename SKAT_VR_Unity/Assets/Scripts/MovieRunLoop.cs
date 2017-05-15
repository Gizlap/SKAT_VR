using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieRunLoop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void Awake(){
		((MovieTexture)GetComponent<Renderer> ().material.mainTexture).Play ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
