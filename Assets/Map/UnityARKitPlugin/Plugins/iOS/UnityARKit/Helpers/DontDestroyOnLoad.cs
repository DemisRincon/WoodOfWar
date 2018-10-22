using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {
    public string tag;
        // Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectWithTag(tag))
        {

        }
        DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
