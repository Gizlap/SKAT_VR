using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentController : MonoBehaviour {

    //public Printer PrintObject;

    public TextController tControl;

    public GameObject AppPrefab;
    public GameObject DenPrefab;

    public bool stampable = false;

    public bool stamped = false;

    public int documentId = -1;

    public ScoreController scoreTrack;

    public StampVariation StampStatus { get; private set; }

    // Use this for initialization
    void Start () {
        StampStatus = StampVariation.NoStamp;
    }

    /**
     * Should only be called once per document
     */
    public void DisableStamping(){
        stampable = false;
        scoreTrack.AddScore (StampStatus, documentId);
    }

    public void EnableStamping(){
        stampable = true;
    }

    public void SetText(int docId, string text){
        documentId = docId;
        tControl.SetText (text);
    }

    // 1
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log(string.Format("TriggerEnter{0}", ""));

        Stamp stampedStamp = other.GetComponent<Stamp> ();
        if (stampable && stampedStamp != null) {
            if(stampedStamp.Stampable){

                //Activate the stamps actions
                stampedStamp.StampAction();

                //Mark Document as stamped
                StampStatus = stampedStamp.variation;
                stamped = true;

                //Create stamp object prefab
                GameObject stamp;
                if (stampedStamp.variation == StampVariation.Approved) {
                    stamp = Instantiate (AppPrefab);
                } else if (stampedStamp.variation == StampVariation.Denied) {
                    stamp = Instantiate (DenPrefab);
                } else {
                    //this would be an error
                    stamp = null;
                }

                //position stamp object prefab
                Vector3 stampPos = other.transform.position;

                stamp.transform.parent = this.transform;

                stamp.transform.position = new Vector3(stampPos.x, this.transform.position.y+0.0005f, stampPos.z);
                stamp.transform.Rotate(new Vector3(270f, 180f, 0f));
                stamp.transform.localScale = new Vector3 (0.39f, 1f, 0.26f);
            }
        }
    }

    // 2
    public void OnTriggerStay(Collider other)
    {
        //Debug.Log(string.Format("TriggerStay {0}", accept_field));
        //stampDetect.LightUp((accept_field?right:left));
    }

    // 3
    public void OnTriggerExit(Collider other)
    {

        //Debug.Log(string.Format("TriggerExit {0}", accept_field));
        //stampDetect.LightDown();
    }

    // Update is called once per frame
    void Update () {
    }
}
