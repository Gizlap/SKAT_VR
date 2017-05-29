using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour {

    public StampVariation variation;
    public AudioSource audSrc;

	private Quaternion upDir = Quaternion.LookRotation(Vector3.up);

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

    float angleOff = 25f;

    Vector3 frontedVertical = new Vector3(270f, 0f, 0f);

    public bool CheckStability()
    {
		

        Vector3 curRot = transform.rotation.eulerAngles;

		float axisDegree = Quaternion.Angle (transform.rotation, upDir);
		Debug.Log (string.Format("degrees {0}",axisDegree));

        //between 70-110 on either axis
        ////bool xAxis = curRot.x <= frontedVertical.x + angleOff && curRot.x >= frontedVertical.x - angleOff;
		//Debug.Log(string.Format("x Axis: {0}", curRot.x));
		////bool zAxis = curRot.z <= frontedVertical.z + angleOff && curRot.z >= frontedVertical.z - angleOff;
		//Debug.Log(string.Format("z Axis: {0}", curRot.z));

        //Stability
        //return xAxis && zAxis;
		return axisDegree <= angleOff;
    }

    private float timerStart { get; set; }

    public float timeBetweenStamps = 0.2f;

    public bool CheckTiming()
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
