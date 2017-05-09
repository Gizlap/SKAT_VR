using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.Timers;

public class ScoreController : MonoBehaviour {


	public int Score { get; private set; }

	public JsonController taskList;

	public Printer printController;

	// Use this for initialization
	void Start () {
		
	}

	void Awake (){
		Score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddScore(StampVariation stampResult, int docId){
		bool success = taskList.Evaluate(docId, stampResult);
		if (success) {
			Score++;
		}

		Debug.Log(string.Format("Player score is {0}", Score));

		printController.StartPrint ();
	}
}
