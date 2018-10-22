using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour {

    [SerializeField]
    private int LoadScene;

    public void SceneTransStart()
    {
        StopAllCoroutines();
        SceneManager.LoadSceneAsync(LoadScene);
    }
}
