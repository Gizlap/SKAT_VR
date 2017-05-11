using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoController : MonoBehaviour {

	public MeshRenderer screen;

	private Video activeVideo = Video.NoVideo;

	public Material IntroVid;
	public Material hurry1;
	public Material hurry2;
	public Material hurry3;
	public Material SkatBlank;
	public Material Illegal;

	public Material EndBad;
	public Material EndOkay;
	public Material EndGood;

	public MovieEnd endVideo;

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
		Material newScreen;
		switch (vid) {
		case Video.Intro:
			newScreen = IntroVid;
			break;
		case Video.Hurry1:
			newScreen = hurry1;
			break;
		case Video.Hurry2:
			newScreen = hurry2;
			break;
		case Video.Hurry3:
			newScreen = hurry3;
			break;
		case Video.EndBad:
			newScreen = EndBad;
			break;
		case Video.EndNeutral:
			newScreen = EndOkay;
			break;
		case Video.EndGood:
			newScreen = EndGood;
			break;
		case Video.NoVideo:
			newScreen = SkatBlank;
			break;
		default:
			newScreen = Illegal;
			break;
		}
		screen.material = newScreen;
		((MovieTexture)screen.material.mainTexture).Play ();
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
		if (!NoVideoPlaying() && !((MovieTexture)screen.material.mainTexture).isPlaying) 
		{
			endVideo (activeVideo);
			screen.material = SkatBlank;
			activeVideo = Video.NoVideo;
		}
	}
}

public delegate void MovieEnd(Video vid);
