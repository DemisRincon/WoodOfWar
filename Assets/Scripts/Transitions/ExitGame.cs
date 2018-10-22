using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExitGame : MonoBehaviour {

    public void Quit()
    {
        #if UNITY_EDITOR
         EditorApplication.isPlaying = false;
        #else
		    Application.Quit();
        #endif
    }
}
