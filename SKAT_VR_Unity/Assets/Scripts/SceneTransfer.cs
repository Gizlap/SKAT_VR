using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransferMovie : MonoBehaviour {

    public Renderer screen;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame

    void Awake()
    {
        ((MovieTexture)screen.material.mainTexture).Play();

    }
    void Update()
    {
        if (!((MovieTexture)screen.material.mainTexture).isPlaying)
        {
            SceneManager.LoadScene("Lydrejse", LoadSceneMode.Single);
        }
    }
}
