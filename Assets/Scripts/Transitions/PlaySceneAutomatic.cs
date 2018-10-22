using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneAutomatic : MonoBehaviour {

    public void sceneTransitionAutomatic()
    {
        Debug.Log("escena cargada"+SaveMananger.Instance.actualScene());
        SceneManager.LoadSceneAsync(SaveMananger.Instance.actualScene());

    }
}
