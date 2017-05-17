using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class GameLoopController : MonoBehaviour {

	public float gameTime;
	public float timeIntervalBetweenTasks;
	public float timeBeforeNextPrintStarts;
	public float taskAcceleration; //Lerped per second
	public bool autoNewTasksOnCompletion;

	public AudioSource endGameSound;

	public bool playIntroVideo;

	public TextMesh scoreText;

	public PrintController pControl;
	public VideoController vControl;
	public ScoreController sControl;
	public RadioController rControl;
	public TutorialController tControl;

	private float currentIntervalBetweenTasks;
	private float currentGameTime;

	public bool introPlaying = true;
	public bool tutorialTime = false;
	public bool gameEndActivated = false;

	private float timeUntilNextTask;

	// Use this for initialization
	void Start () {
		//currentIntervalBetweenTasks = 
		currentGameTime = gameTime;
		timeUntilNextTask = timeIntervalBetweenTasks;

		pControl.taskSpeed = taskAcceleration;
		pControl.totalPrintTime = timeIntervalBetweenTasks-timeBeforeNextPrintStarts;
		pControl.SetMoveTime ();

		vControl.endVideo += VideoEnd;


	}

	void Awake(){
		if (playIntroVideo) {
			vControl.PlayVideo (Video.Intro);
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
		else if (!tControl.TutorialComplete) 
		{

			rControl.beginSounds ();
			tControl.StartTutorial ();

			//tutorial.
			//Print out paper with basic instructions

			//Print out paper that have to be stamped Godkendt

			//Print out paper that have to be stamped Rejected
		}
		else if(currentGameTime > 0f)
		{
			pControl.Activate();
			//Debug.Log(string.Format("Game time: {0}, time till next Task: {1}, currentInterval: {2}", currentGameTime, timeUntilNextTask, timeIntervalBetweenTasks));


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
			if (!gameEndActivated) {
				rControl.EndSounds ();
				endGameSound.Play ();
				vControl.ForceNewVideo (Video.GameOver);
				gameEndActivated = true;
			}

			//Game over
		}
	}

	private void ActivateGameEnd ()
	{
		Debug.Log ("GameEnd Activated");

		ScoreEnum s = sControl.GetScoreResult ();

		switch (s) {
		case ScoreEnum.High:
			vControl.PlayVideo (Video.EndGood);
			break;
		case ScoreEnum.Medium:
			vControl.PlayVideo (Video.EndNeutral);
			break;
		case ScoreEnum.Low:
			vControl.PlayVideo (Video.EndBad);
			break;
		}

		scoreText.text = string.Format ("Score: {0} rigtige!", sControl.Score); 

		//TODO

		//Disable Currently active documents

		//Disable Controller
	}

	public void TaskStamped(){

	}


	public void VideoEnd(Video vid){
		switch (vid){
		case (Video.Intro):
			introPlaying = false;
			tutorialTime = true;
			break;
		case (Video.GameOver):
			ActivateGameEnd ();
			break;
		}
	}
}
