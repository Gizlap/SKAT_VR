using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopController : MonoBehaviour {

	public float gameTime = 60.0f;
	public float timeIntervalBetweenTasks = 3.0f;
	public float timeBeforeNextPrintStarts = 0.5f;
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
		//currentIntervalBetweenTasks = 
		currentGameTime = gameTime;
		timeUntilNextTask = timeIntervalBetweenTasks;

		pControl.taskSpeed = taskAcceleration;
		pControl.totalPrintTime = timeIntervalBetweenTasks-timeBeforeNextPrintStarts;
		pControl.SetMoveTime ();

		videoScreen.endVideo += IntroEnd;


	}

	void Awake(){
		if (playIntroVideo) {
			videoScreen.PlayIntro ();
			introPlaying = true;
		} else {
			introPlaying = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		

		if (introPlaying) 
		{
			
			//Intro running
			//Do nothing more
		}
		else if(currentGameTime > 0f)
		{
			pControl.Activate();
			Debug.Log(string.Format("Game time: {0}, time till next Task: {1}, currentInterval: {2}", currentGameTime, timeUntilNextTask, timeIntervalBetweenTasks));


			//Game running
			timeUntilNextTask -= Time.deltaTime;
			if (timeUntilNextTask <= 0f) {
				pControl.StartPrint();
				timeUntilNextTask = timeIntervalBetweenTasks;
			}

			//Speed adjastment
			timeIntervalBetweenTasks =  timeIntervalBetweenTasks / (1 + (taskAcceleration * Time.deltaTime));

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
