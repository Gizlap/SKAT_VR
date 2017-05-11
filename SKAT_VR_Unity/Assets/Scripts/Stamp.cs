using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;

	public StampVariation variation;

	public AudioSource audSrc;

	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	// Use this for initialization
	void Start () {

	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
