using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintController : MonoBehaviour {

    public GameObject blanket;

    public JsonController json;
    public ScoreController scoreCont;

    //public float printSpeedFactor = 0.1f;

    public Transform first;
    public Transform second;
    public Transform third;
    public Transform final;
    public Transform revPos;

	public float TaskSpeed { get; set; }
	public float TotalPrintTime { get; set; }

    public float firstMove;
    private float firstMoveActual;
    public float secondMove;
    private float secondMoveActual;
    public float thirdMove;
    private float thirdMoveActual;

    private float startTime;

    DocumentController newTask;
    public DocumentController activeDocument;

    private bool printActive = false;
    private bool disabledPrevious = false;

    private bool activated = false;

    // Use this for initialization
    void Start () {
        
    }

    void Awake () {
        //StartPrint ();
        ResetMoves();

    }

    public bool CurrentlyPrinting { get { return printActive; } }

    public void Activate(){
        activated = true;
    }

    public void SetMoveTime(){
        float totalMove = firstMove+secondMove+thirdMove;
        firstMove = firstMove / totalMove * TotalPrintTime;
        secondMove = secondMove / totalMove * TotalPrintTime;
        thirdMove = thirdMove / totalMove * TotalPrintTime;
        //Debug.Log(string.Format("{0}, {1}, {2}",firstMove, secondMove, thirdMove));
    }

    public void ResetMoves()
    {
        //SetMoveTime();
        firstMoveActual = firstMove;
        secondMoveActual = secondMove;
        thirdMoveActual = thirdMove;
    }

    // Update is called once per frame
    void Update () {
        if(activated){
            firstMoveActual = firstMoveActual / (1 + (TaskSpeed * Time.deltaTime));
            secondMoveActual = secondMoveActual / (1 + (TaskSpeed * Time.deltaTime));
            thirdMoveActual = thirdMoveActual / (1 + (TaskSpeed * Time.deltaTime));

            /*if (Time.time > 1f && firstPrint) {
                StartPrint ();
                firstPrint = false;
            }*/

            if (printActive)
            {
                float t = Time.time - startTime;
                //Debug.Log(string.Format("t: {0}", t));
                float curT = 0f;
                if (t <= firstMoveActual)
                {
                    curT = t / firstMoveActual;
                    newTask.transform.position = Vector3.Lerp(first.position, second.position, curT);
                    newTask.transform.rotation = Quaternion.Lerp(first.rotation, second.rotation, curT);
                }
                else if (t <= firstMoveActual + secondMoveActual)
                {
                    curT = (t - (firstMoveActual)) / secondMoveActual;
                    newTask.transform.position = Vector3.Lerp(second.position, third.position, curT);
                    newTask.transform.rotation = Quaternion.Lerp(second.rotation, third.rotation, curT);
                }
                else if (t <= firstMoveActual + secondMoveActual + thirdMoveActual)
                {
                    curT = (t - (firstMoveActual + secondMoveActual)) / thirdMoveActual;
                    newTask.transform.position = Vector3.Lerp(third.position, final.position, curT);
                    newTask.transform.rotation = Quaternion.Lerp(third.rotation, final.rotation, curT);

                    if (!disabledPrevious)
                    {
                        //switch moving paper to active for stamping and current paper to non-active.
                        if (activeDocument != null)
                        {
                            activeDocument.DisableStamping();
                        }

                        disabledPrevious = true;
                    }
                }
                else
                {
                    newTask.transform.position = final.position;
                    newTask.transform.rotation = final.rotation;
                    newTask.EnableStamping();

                    activeDocument.transform.position = revPos.position;
                    activeDocument.gameObject.SetActive(false);

                    activeDocument = newTask.GetComponent<DocumentController>();

                    printActive = false;
                    disabledPrevious = false;
                }
                //Debug.Log(curT);
            }
        }
    }

    public void StartPrint(string text, int id){
        engagePrinter (text, id);
        //newTask.
    }

    private int printInt = 0;

    public void StartPrint(){

        if (!printActive && activated) {
            string text = json.GetTaskOrder (printInt);
            engagePrinter (text, printInt);
            printInt++;
        }
        //int id = -1;
        //string text = json.GetTask (out id);
        //engagePrinter (text, id);
    }

    void engagePrinter (string text, int id)
    {
        if (!printActive && activated) {
            startTime = Time.time;
            printActive = true;
            GameObject obj = Instantiate (blanket);
            newTask = obj.GetComponent<DocumentController> ();
            newTask.SetText (id, text);
            newTask.transform.position = first.position;
            newTask.scoreTrack = scoreCont;
            //TODO
            //newTask.SetText (-1, "Udbetaling af udbytteskat til skuffeselskabet 'Svindell og Søn ApS' af 10.000.000 kr.");
        }
    }
}
