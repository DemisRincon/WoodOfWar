using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveTrigger : MonoBehaviour {
    [SerializeField]
    public GameObject panelPlayer;
    [SerializeField]
    public GameObject loadingScreen;
    [SerializeField]
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        panelPlayer.SetActive(false);
        loadingScreen.SetActive(true);
        
        SaveMananger.Instance.scenePassed();
        SaveMananger.Instance.saveItems(GameManager.Instance.CollectedCans, GameManager.Instance.CollectedBottles, GameManager.Instance.CollectedBags);
        
    }
}
