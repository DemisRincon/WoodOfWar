using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelector : MonoBehaviour {

    public GameObject modeContainer;

    public void selectorMode()
    {
        if (SaveMananger.Instance.modeConsult())
        {
            modeContainer.GetComponent<PlaySceneAutomatic>().sceneTransitionAutomatic();
        }
        else
        {
            modeContainer.GetComponent<PlayGame>().SceneTransStart();
        }
    }
}
