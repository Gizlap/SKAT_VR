using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.Timers;

public class ScoreController : MonoBehaviour {


	public int Score { get; private set; }

	public JsonController taskList;

	public int lowScore;
	public int highScore;

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
		if (stampResult != StampVariation.NoStamp) {
			if (taskList.Evaluate(docId, stampResult)) {
				Score++;
			}
		}

		Debug.Log(string.Format("Player score is {0}", Score));
	}

	public ScoreEnum GetScoreResult(){
		if (Score > highScore) {
			return ScoreEnum.High;
		} else if (Score > lowScore) {
			return ScoreEnum.Medium;
		} else {
			return ScoreEnum.Low;
		}
	}
}
