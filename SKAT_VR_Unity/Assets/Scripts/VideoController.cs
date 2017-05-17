using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VideoController : MonoBehaviour {

	public GameObject BriefScreen;
	public GameObject GameOverScreen;

	public GameObject ActiveScreen;


	public MeshRenderer screen;

	private Video activeVideo = Video.NoVideo;

	public Material IntroVid;
	public Material[] hurryVids;
	public Material GameOver;

	public Material SkatBlank;
	public Material Illegal;

	public Material EndBad;
	public Material EndOkay;
	public Material EndGood;

	public MovieEnd endVideo;

	private System.Random rand = new System.Random();

	//private bool videoPlaying = false;

	// Use this for initialization
	void Start () {
		
	}

	void Awake () {
	}

	bool NoVideoPlaying ()
	{
		return activeVideo == Video.NoVideo;
	}

	/// <summary>
	/// Forces a new Video to run immidiately, ending the other video.
	/// Do not use this unless you have to, use PlayVideo instead
	/// </summary>
	/// <param name="vid">The Video to play</param>

	public void ForceNewVideo (Video vid)
	{
		endVideo (activeVideo);

		VideoPlay (vid);
		


	}

	public void PlayVideo(Video vid){
		if (NoVideoPlaying ()) {
			VideoPlay (vid);
		}
	}

	private void VideoPlay (Video vid)
	{
		activeVideo = vid;
		//Material newScreen;
		switch (vid) {
		case Video.Intro:
			
			//newScreen = IntroVid;
			ActiveScreen = BriefScreen;
			break;
		case Video.HurryVid:
			//newScreen = hurryVids[rand.Next(hurryVids.Length)];
			break;
		case Video.EndBad:
			//newScreen = EndBad;
			break;
		case Video.EndNeutral:
			//newScreen = EndOkay;
			break;
		case Video.EndGood:
			//newScreen = EndGood;
			break;
		case Video.NoVideo:
			//newScreen = SkatBlank;
			break;
		case Video.GameOver:
			//newScreen = GameOver;
			ActiveScreen = GameOverScreen;
			break;
		default:
			//newScreen = Illegal;
			break;
		}

		ActiveScreen.SetActive (true);
		//screen.material = newScreen;
		//((MovieTexture)screen.material.mainTexture).Play ();
	}
		
	/*public void PlayIntro(){
		if (!videoPlaying) {
			screen.material = IntroVid;
			((MovieTexture)screen.material.mainTexture).Play ();
			videoPlaying = true;
		}
	}*/
	
	// Update is called once per frame
	void Update () {
		if (!NoVideoPlaying() && !((MovieTexture)ActiveScreen.GetComponent<Renderer>().material.mainTexture).isPlaying)//!((MovieTexture)screen.material.mainTexture).isPlaying) 
		{
			Video remVid = activeVideo; 
			activeVideo = Video.NoVideo;
			ActiveScreen.SetActive (false);
			ActiveScreen = null;
			//screen.material = SkatBlank;
			endVideo (remVid);
		}
	}
}

public delegate void MovieEnd(Video vid);
