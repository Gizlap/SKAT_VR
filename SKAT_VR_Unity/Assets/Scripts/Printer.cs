using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour {

	public GameObject blanket;

	public JsonController json;
	public ScoreController scoreCont;

	//public float printSpeedFactor = 0.1f;

	public Transform first;
	public Transform second;
	public Transform third;
	public Transform final;
	public Transform revPos;

	public float taskSpeed;
	public float firstMove;
	public float secondMove;
	public float thirdMove;
	private float startTime;

	DocumentController newTask;
	public DocumentController activeDocument;

	private bool printActive = false;
	private bool disabledPrevious = false;

	private float firstMoveCur;
	private float secondMoveCur;
	private float thirdMoveCur;

	// Use this for initialization
	void Start () {
		
	}

	void Awake () {
		//StartPrint ();
	}

	// Update is called once per frame
	void Update () {
		firstMove = firstMove / (1 + (taskSpeed * Time.deltaTime));
		secondMove = secondMove / (1 + (taskSpeed * Time.deltaTime));
		thirdMove = thirdMove / (1 + (taskSpeed * Time.deltaTime));

		/*if (Time.time > 1f && firstPrint) {
			StartPrint ();
			firstPrint = false;
		}*/

		if (printActive) {
			float t = Time.time - startTime;
			//Debug.Log(string.Format("t: {0}", t));
			float curT = 0f;
			if(t <= firstMove) {
				curT = t / firstMove;
				newTask.transform.position = Vector3.Lerp(first.position, second.position, curT);
				newTask.transform.rotation = Quaternion.Lerp (first.rotation, second.rotation, curT);
			} else if (t <= firstMove + secondMove) {
				curT = (t - (firstMove)) / secondMove;
				newTask.transform.position = Vector3.Lerp (second.position, third.position, curT);					
				newTask.transform.rotation = Quaternion.Lerp (second.rotation, third.rotation, curT);
			} else if (t <= firstMove + secondMove + thirdMove) {
				curT = (t - (firstMove + secondMove)) / thirdMove;
				newTask.transform.position = Vector3.Lerp (third.position, final.position, curT);
				newTask.transform.rotation = Quaternion.Lerp (third.rotation, final.rotation, curT);

				if (!disabledPrevious) {
					//switch moving paper to active for stamping and current paper to non-active.
					if (activeDocument != null) {
						activeDocument.DisableStamping ();
					}

					disabledPrevious = true;
				}
			} else {
				newTask.transform.position = final.position;
				newTask.transform.rotation = final.rotation;
				newTask.EnableStamping ();

				activeDocument.transform.position = revPos.position;

				activeDocument = newTask.GetComponent<DocumentController> ();

				printActive = false;
				disabledPrevious = false;
			}
			Debug.Log(curT);
		}
	}

	public void StartPrint(){
		if(!printActive){
			startTime = Time.time;
			printActive = true;

			GameObject obj = Instantiate (blanket);

			int id = -1;

			string text = json.GetTask (out id);

			newTask = obj.GetComponent<DocumentController> ();

			newTask.SetText (id, text);

			newTask.transform.position = first.position;

			newTask.scoreTrack = scoreCont;

			//TODO
			//newTask.SetText (-1, "Udbetaling af udbytteskat til skuffeselskabet 'Svindell og Søn ApS' af 10.000.000 kr.");
		}
	}
}
