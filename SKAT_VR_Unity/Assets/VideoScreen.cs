using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoScreen : MonoBehaviour {

	public MeshRenderer screen;

	// Use this for initialization
	void Start () {
		
	}

	void Awake () {
		PlayIntro ();
	}

	public void PlayIntro(){
		((MovieTexture)screen.material.mainTexture).Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
