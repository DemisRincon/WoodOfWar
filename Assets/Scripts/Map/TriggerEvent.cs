using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour {

    public GameObject buttonPlay;
    public GameObject textPanelWalk;
    public GameObject textPlay;
    private void OnTriggerStay(Collider other)
    {
     
       
    }
    private void OnTriggerEnter(Collider other)
    {
        buttonPlay.SetActive(true);
        textPanelWalk.SetActive(false);
        textPlay.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        buttonPlay.SetActive(false);
        textPanelWalk.SetActive(true);
        textPlay.SetActive(false);
    }
}

