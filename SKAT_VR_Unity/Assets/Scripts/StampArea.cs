using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampArea : MonoBehaviour {

	//public StampDetection stampDetect;

	public Renderer plane;

	//public bool accept_field;

	//public Material right;
	//public Material left;

	public Material AppTexture;
	public Material DenTexture;

	public GameObject AppPrefab;
	public GameObject DenPrefab;

	// Use this for initialization
	void Start () {
		
	}

	// 1
	public void OnTriggerEnter(Collider other)
	{
		Debug.Log(string.Format("TriggerEnter{0}", ""));
		//stampDetect.LightUp((accept_field?right:left));
		//stampDetect.other.transform.position

		Stamp stampedStamp = other.GetComponent<Stamp> ();
		if (stampedStamp != null) {
			GameObject stamp;
			if (stampedStamp.variation == StampVariation.Approved) {
				stamp = Instantiate (AppPrefab);
				//plane.material = AppTexture;
			} else if (stampedStamp.variation == StampVariation.Denied) {
				stamp = Instantiate (DenPrefab);
				//plane.material = DenTexture;
			} else {
				stamp = null;
			}

			Vector3 stampPos = other.transform.position;

			stamp.transform.parent = this.transform;

			//plane.transform.position = new Vector3(stampPos.x, this.transform.position.y+0.01f, stampPos.z);
			stamp.transform.position = new Vector3(stampPos.x, this.transform.position.y+0.01f, stampPos.z);
			stamp.transform.Rotate(new Vector3(270f, 180f, 0f));
			stamp.transform.localScale = new Vector3 (0.3f, 1f, 0.2f);

			//plane.gameObject.SetActive (true);
		}
	}

	// 2
	public void OnTriggerStay(Collider other)
	{
		//Debug.Log(string.Format("TriggerStay {0}", accept_field));
		//stampDetect.LightUp((accept_field?right:left));
	}

	// 3
	public void OnTriggerExit(Collider other)
	{

		//Debug.Log(string.Format("TriggerExit {0}", accept_field));
		//stampDetect.LightDown();
	}
	// Update is called once per frame
	void Update () {
	}
}
