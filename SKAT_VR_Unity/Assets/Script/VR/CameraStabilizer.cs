using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStabilizer : MonoBehaviour {

	//private Vector3 InitialPos;
	//private Vector3 hmdPos;
	public GameObject HMD;
	public GameObject VRRig;
	public Transform WantedCameraPos;

	void Start ()
	{
		Vector3 moveHMD = HMD.transform.position - WantedCameraPos.position;

		VRRig.transform.position = VRRig.transform.position - moveHMD;


		//HMD.transform.position.x;
		
	}

	// Update is called once per frame
	void LateUpdate ()
	{
		//hmdPos = HMD.transform.position;                         Can't work in world position
		//transform.position = InitialPos - hmdPos;                for some reason
		//hmdPos = HMD.transform.localPosition;
		//transform.position = CameraPos.position - hmdPos;
	}


}
