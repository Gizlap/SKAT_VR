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
	public VideoScreen videoScreen;

	private float currentIntervalBetweenTasks;
	private float currentGameTime;

	public bool introPlaying = true;

	// Use this for initialization
	void Start () {
		currentIntervalBetweenTasks = timeIntervalBetweenTasks;
		currentGameTime = gameTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (introPlaying) 
		{
			//Intro running
		}
		else if(currentGameTime > 0f)
		{
			//Game running

			//Speed adjestment
			currentIntervalBetweenTasks = timeIntervalBetweenTasks*((gameTime - currentGameTime)*taskAcceleration);
			pControl.speed += taskAcceleration * Time.deltaTime;

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
}
