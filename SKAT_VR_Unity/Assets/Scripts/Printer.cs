using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour {

	public GameObject blanket;

	public float printSpeedFactor = 0.1f;

	public Transform first;
	public Transform second;
	public Transform third;
	public Transform final;

	public float speed = 1.0f;
	public float firstMove = 1.8f;
	public float secondMove = 1.0f;
	public float thirdMove = 1.0f;
	private float startTime;
	private float curTime;

	GameObject newTask;

	//private float journeyLengthFirst;
	//private float journeyLengthSecond;

	private bool printActive = false;
	private bool firstPrint = true;

	// Use this for initialization
	void Start () {
		
	}

	void Awake () {
		//StartPrint ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > 5f && firstPrint) {
			StartPrint ();
			firstPrint = false;
		}

		if (printActive) {
			curTime = Time.time;
			float t = curTime - startTime;
			Debug.Log(string.Format("t: {0}", t));
			if(t <= firstMove) {
				newTask.transform.position = Vector3.Lerp(first.position, second.position, (t/firstMove));
				//newTask.transform.eulerAngles = new Vector3(Mathf.LerpAngle(first.eulerAngles.x, second.eulerAngles.x, (t/firstMove)),
				//											Mathf.LerpAngle(first.eulerAngles.y, second.eulerAngles.y, (t/firstMove)),
				//											Mathf.LerpAngle(first.eulerAngles.z, second.eulerAngles.z, (t/firstMove)));

				newTask.transform.rotation = Quaternion.Lerp (first.rotation, second.rotation, (t/firstMove));
			} else if (t <= secondMove + firstMove) {
				newTask.transform.position = Vector3.Lerp (second.position, third.position, ((t - (firstMove)) / (secondMove)));

				//newTask.transform.eulerAngles = new Vector3(Mathf.LerpAngle(second.eulerAngles.x, final.eulerAngles.x, (t/(firstMove+secondMove))),
					
				newTask.transform.rotation = Quaternion.Lerp (second.rotation, third.rotation, ((t - (firstMove)) / (secondMove)));
			} else if (t <= secondMove + firstMove + thirdMove) {
				newTask.transform.position = Vector3.Lerp (third.position, final.position, ((t - (firstMove+secondMove)) / (thirdMove)));

				//newTask.transform.eulerAngles = new Vector3(Mathf.LerpAngle(second.eulerAngles.x, final.eulerAngles.x, (t/(firstMove+secondMove))),

				newTask.transform.rotation = Quaternion.Lerp (third.rotation, final.rotation, ((t - (firstMove+secondMove)) / (thirdMove)));
			} else {
				printActive = false;
			}
		}
	}

	public void StartPrint(){
		startTime = Time.time;
		printActive = true;

		//journeyLengthFirst = Vector3.Distance (first.position, second.position);
		//journeyLengthSecond = Vector3.Distance (second.position, final.position);

		newTask = Instantiate (blanket);

		newTask.transform.position = first.position;

		//newTask.transform.rotation = Vector3 (170f, 180f, 0f);

	}
}
