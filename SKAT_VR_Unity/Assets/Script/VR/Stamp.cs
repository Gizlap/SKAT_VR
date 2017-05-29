using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour {

    public StampVariation variation;
    public AudioSource audSrc;

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    // Use this for initialization
    void Start () {

    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Shake (){
        Controller.TriggerHapticPulse (3999);
        //Controller.
    }


    
    // Update is called once per frame
    void Update () {
        
    }

    float angleOff = 20f;

    Vector3 frontedVertical = new Vector3(0f, 0f, 0f);

    private bool CheckStability()
    {
        Vector3 curRot = transform.rotation.eulerAngles;
        //between 70-110 on either axis
        bool xAxis = curRot.x <= frontedVertical.x + angleOff && curRot.x >= frontedVertical.x - angleOff;
        bool zAxis = curRot.z <= frontedVertical.z + angleOff && curRot.z >= frontedVertical.z - angleOff;

        //Stability
        return xAxis && zAxis;
    }

    private float timerStart { get; set; }

    public float timeBetweenStamps = 0.2f;

    private bool CheckTiming()
    {
        return (Time.time >= timerStart + timeBetweenStamps);
        //Timing
    }

    public bool Stampable { get { return CheckStability() && CheckTiming(); } }

    public bool StampAction()
    {
        timerStart = Time.time;
        audSrc.Play();
        Shake();
        return true;
    }
}
