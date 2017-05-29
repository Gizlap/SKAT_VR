using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class GameLoopController : MonoBehaviour {

    public float gameTime;
    public float timeIntervalBetweenTasks;
    public float timeBeforeNextPrintStarts;
    public float taskAcceleration; //Lerped per second
    public bool autoNewTaskOnStamping;
    public bool autoNewTaskOnTimer;

    public AudioSource endGameSound;

    public bool skipIntroVideo;
	public bool skipTutorial;

    public TextMesh scoreText;

    public PrintController pControl;
    public VideoController vControl;
    public ScoreController sControl;
    public RadioController rControl;
    public TutorialController tControl;

    private float currentIntervalBetweenTasks;
    private float currentGameTime;

    public bool IntroPlaying { get; private set; }
    public bool OnelinersStarted { get; private set; }
    public bool GameEndActivated { get; private set; }

    private float timeUntilNextTask;

    // Use this for initialization
    void Start () {
        OnelinersStarted = false;
        GameEndActivated = false;

        //currentIntervalBetweenTasks = 
        currentGameTime = gameTime;
        timeUntilNextTask = timeIntervalBetweenTasks;

        pControl.TaskSpeed = taskAcceleration;
        pControl.TotalPrintTime = timeIntervalBetweenTasks-timeBeforeNextPrintStarts;
        pControl.SetMoveTime ();

        vControl.endVideo += VideoEnd;
    }

    void Awake(){
		if (skipIntroVideo) {
			IntroPlaying = false;
		} else {
			vControl.PlayVideo (Video.Intro);
			IntroPlaying = true;
        }
    }
    
    // Update is called once per frame
    void Update () {
        

        if (IntroPlaying) 
        {
            //Intro running
            //Do nothing more
        } 
		else if (!tControl.TutorialComplete) 
        {
			rControl.beginSounds ();
			tControl.StartTutorial (skipTutorial);
        }
        else if(currentGameTime > 0f)
        {
            if (!OnelinersStarted) {
				Debug.Log ("Game Started");

                vControl.PlayVideo (Video.HurryVid);
                pControl.ResetMoves();
				pControl.StartPrint();

				OnelinersStarted = true;
            }
            
            //pControl.Activate();
            //Debug.Log(string.Format("Game time: {0}, time till next Task: {1}, currentInterval: {2}", currentGameTime, timeUntilNextTask, timeIntervalBetweenTasks));


            //Game running
            timeUntilNextTask -= Time.deltaTime;
            if (autoNewTaskOnTimer && timeUntilNextTask <= 0f && !pControl.CurrentlyPrinting) 
			{
				Debug.Log ("TimerTaskStarted");
                pControl.StartPrint();
                timeUntilNextTask = timeIntervalBetweenTasks;
            }

            if (autoNewTaskOnStamping && pControl.activeDocument.StampStatus != StampVariation.NoStamp && !pControl.CurrentlyPrinting)
			{
				Debug.Log ("StampTaskStarted");
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
            if (!GameEndActivated) {
                rControl.EndSounds ();
                endGameSound.Play ();
                vControl.ForceNewVideo (Video.GameOver);
                GameEndActivated = true;
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
        scoreText.gameObject.SetActive (true);
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
            IntroPlaying = false;
            break;
        case (Video.GameOver):
            ActivateGameEnd ();
            break;
        case (Video.EndBad):
        case (Video.EndGood):
        case (Video.EndNeutral):
            Application.Quit();
            break;
        }

    }
}
