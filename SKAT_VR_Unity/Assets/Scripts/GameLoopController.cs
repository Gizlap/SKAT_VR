using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopController : MonoBehaviour {

	public float gameTime = 60.0f;
	public float timeIntervalBetweenTasks = 3.0f;
	public float taskAcceleration = 0.01f; //Lerped per second
	public bool autoNewTasksOnCompletion = false;

	public bool playIntroVideo = true;


	public Printer pControl;
	public VideoScreen videoScreen;

	private float currentIntervalBetweenTasks;
	private float currentGameTime;

	public bool introPlaying = true;

	private float timeUntilNextTask;

	// Use this for initialization
	void Start () {
		currentIntervalBetweenTasks = timeIntervalBetweenTasks;
		currentGameTime = gameTime;
		timeUntilNextTask = currentIntervalBetweenTasks;

		pControl.taskSpeed = taskAcceleration;

		videoScreen.endVideo += IntroEnd;
	}
	
	// Update is called once per frame
	void Update () {
		if (introPlaying) 
		{
			introPlaying = false;
			//Intro running
			//Do nothing more
		}
		else if(currentGameTime > 0f)
		{
			//Debug.Log(string.Format("Game time: {0}, time till next Task: {1}, currentInterval: {2}", currentGameTime, timeUntilNextTask, currentIntervalBetweenTasks));


			//Game running
			timeUntilNextTask -= Time.deltaTime;
			if (timeUntilNextTask <= 0f) {
				pControl.StartPrint();
				timeUntilNextTask = currentIntervalBetweenTasks;
			}

			//Speed adjestment
			currentIntervalBetweenTasks = timeIntervalBetweenTasks-((gameTime - currentGameTime)*taskAcceleration);
			pControl.taskSpeed += taskAcceleration * Time.deltaTime;

			//updateTime
			currentGameTime -= Time.deltaTime;
		}
		else
		{
			//Game over
		}
	}

	public void TaskStamped(){

	}

	public void IntroEnd(){
		introPlaying = false;
	}
}
