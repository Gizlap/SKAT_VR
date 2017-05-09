using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour {

	public GameObject blanket;

	//public float printSpeedFactor = 0.1f;

	public Transform first;
	public Transform second;
	public Transform third;
	public Transform final;

	public float speed;
	public float firstMove;
	public float secondMove;
	public float thirdMove;
	private float startTime;
	private float curTime;

	DocumentController newTask;
	public DocumentController activeDocument;

	//private float journeyLengthFirst;
	//private float journeyLengthSecond;

	private bool printActive = false;
	private bool firstPrint = true;
	private bool activeSwitched = false;

	// Use this for initialization
	void Start () {
		
	}

	void Awake () {
		//StartPrint ();
	}

	// Update is called once per frame
	void Update () {
		if (Time.time > 1f && firstPrint) {
			StartPrint ();
			firstPrint = false;
		}

		if (printActive) {
			curTime = Time.time;
			float t = curTime - startTime;
			//Debug.Log(string.Format("t: {0}", t));
			if(t <= firstMove) {
				newTask.transform.position = Vector3.Lerp(first.position, second.position, (t/(firstMove*speed)));
				newTask.transform.rotation = Quaternion.Lerp (first.rotation, second.rotation, (t/(firstMove*speed)));
			} else if (t <= secondMove + firstMove) {
				newTask.transform.position = Vector3.Lerp (second.position, third.position, ((t - (firstMove)) / (secondMove*speed)));					
				newTask.transform.rotation = Quaternion.Lerp (second.rotation, third.rotation, ((t - (firstMove)) / (secondMove*speed)));
			} else if (t <= secondMove + firstMove + thirdMove) {
				newTask.transform.position = Vector3.Lerp (third.position, final.position, ((t - (firstMove+secondMove)) / (thirdMove*speed)));
				newTask.transform.rotation = Quaternion.Lerp (third.rotation, final.rotation, ((t - (firstMove+secondMove)) / (thirdMove*speed)));

				if (!activeSwitched) {
					//switch moving paper to active for stamping and current paper to non-active.
					if (activeDocument != null) {
						activeDocument.DisableStamping ();
					}
					activeDocument = newTask.GetComponent<DocumentController> ();

					activeSwitched = true;
				}
			} else {
				activeDocument.EnableStamping ();
				printActive = false;
				activeSwitched = false;
			}
		}
	}

	public void StartPrint(){
		startTime = Time.time;
		printActive = true;

		GameObject obj = Instantiate (blanket);

		newTask = obj.GetComponent<DocumentController> ();

		newTask.transform.position = first.position;

		//TODO
		newTask.SetText (-1, "text");

	}
}
