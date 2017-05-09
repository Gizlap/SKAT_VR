using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentController : MonoBehaviour {

	//public Printer PrintObject;

	public TextController tControl;

	public GameObject AppPrefab;
	public GameObject DenPrefab;

	public bool stampable = false;

	private bool stamped = false;

	public int documentId = -1;

	public ScoreController scoreTrack;

	private StampVariation stampStatus;

	// Use this for initialization
	void Start () {
		
	}

	/**
	 * Should only be called once per document
	 */
	public void DisableStamping(){
		stampable = false;
		//TODO
		//scoreTrack.AddScore (stampStatus, documentId);
	}

	public void EnableStamping(){
		stampable = true;
	}

	public void SetText(int docId, string text){
		documentId = docId;
		tControl.SetText (text);
	}

	// 1
	public void OnTriggerEnter(Collider other)
	{
		Debug.Log(string.Format("TriggerEnter{0}", ""));
		//stampDetect.LightUp((accept_field?right:left));
		//stampDetect.other.transform.position

		Stamp stampedStamp = other.GetComponent<Stamp> ();
		if (stampable && stampedStamp != null) {
			stampStatus = stampedStamp.variation;
			stamped = true;

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
			stamp.transform.position = new Vector3(stampPos.x, this.transform.position.y+0.005f, stampPos.z);
			stamp.transform.Rotate(new Vector3(270f, 180f, 0f));
			stamp.transform.localScale = new Vector3 (0.39f, 1f, 0.26f);

			//PrintObject.StartPrint ();

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
