using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoScreen : MonoBehaviour {

	public MeshRenderer screen;

	public Material IntroVid;
	public Material hurry1;
	public Material hurry2;
	public Material hurry3;
	public Material SkatBlank;

	public Material EndBad;
	public Material EndOkay;
	public Material EndGood;

	public MovieEnd endVideo;

	private bool videoPlaying = false;

	// Use this for initialization
	void Start () {
		
	}

	void Awake () {
	}

	public void PlayIntro(){
		if (!videoPlaying) {
			screen.material = IntroVid;
			((MovieTexture)screen.material.mainTexture).Play ();
			videoPlaying = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (videoPlaying && !((MovieTexture)screen.material.mainTexture).isPlaying) {
			endVideo ();
			screen.material = SkatBlank;
			videoPlaying = false;
		}
	}
}

public delegate void MovieEnd();
