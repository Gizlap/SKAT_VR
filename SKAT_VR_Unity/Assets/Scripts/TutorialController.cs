using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {

	public PrintController pControl;

	public bool TutorialComplete { get; private set; }

	private bool tutorialStarted = false;

	private bool firstPrinted = false;

	private bool taskOne = false;
	private bool taskTwo = false;
	private bool taskThree = false;

	// Use this for initialization
	void Start () {
		TutorialComplete = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (tutorialStarted) {

			if (!firstPrinted) {
				pControl.Activate ();
				pControl.StartPrint ("tutorialInstructions", -1);
				firstPrinted = true;
			}
			
			if (!taskOne) {
				//Instructions, Stamp Anything to continue
				if (pControl.activeDocument.documentId == -1 && pControl.activeDocument.stamped) {
					taskOne = true;
					pControl.StartPrint ("stamp Godkendt", -2);
				}
			} else if (!taskTwo) {

				//Stamp Godkendt to continue
				if (pControl.activeDocument.documentId == -2 && pControl.activeDocument.StampStatus == StampVariation.Approved) {
					taskTwo = true;
					pControl.StartPrint ("stamp Afvist", -3);
				}
			} else if (!taskThree) {

				//Stamp Afvist to continue
				if (pControl.activeDocument.documentId == -3 && pControl.activeDocument.StampStatus == StampVariation.Denied) {
					taskThree = true;
				}
			}

			if (taskOne && taskTwo && taskThree) {
				TutorialComplete = true;
			}
		}
	}

	public void StartTutorial(){
		if (tutorialStarted) {
			return;
		}

		tutorialStarted = true;
	}

	private void PaperReturn(TutorialEnum tEn){
		switch(tEn){
		case TutorialEnum.TaskOne:
			break;
		case TutorialEnum.TaskTwo:
			break;
		case TutorialEnum.TaskThree:
			break;
		}
	}
}

public delegate void TutorialReturn(TutorialEnum tEn);

public enum TutorialEnum{
	TaskOne, TaskTwo, TaskThree
}