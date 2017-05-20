using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieRunLoop : MonoBehaviour {

    MovieTexture screen;

    // Use this for initialization
    void Start () {
        
    }

    void Awake(){
        screen = (MovieTexture)GetComponent<Renderer> ().material.mainTexture;
        screen.Play();
    }
    
    // Update is called once per frame
    void Update () {
        if (!screen.isPlaying)
        {
            screen.Play();
        }
    }
}
