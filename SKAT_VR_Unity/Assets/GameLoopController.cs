using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopController : MonoBehaviour {

	public float gameTime = 60.0f;
	public float timeIntervalBetweenTasks = 2.0f;
	public float taskAcceleration = 0.01f; //Lerped per second
	public bool autoNewTasksOnCompletion = true;

	public bool playIntroVideo = true;


	public Printer pControl;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TaskStamped(){

	}
}
