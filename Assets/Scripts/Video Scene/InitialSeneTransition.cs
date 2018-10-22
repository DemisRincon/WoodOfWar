using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSeneTransition : MonoBehaviour {

    [SerializeField]
    private int LoadScene;

    public void StopRoutinesAndLoad()
    {
        StopAllCoroutines();
        StreamVideo.Instance.videoPlayer.Stop();
        StreamVideo.Instance.audioSource.Stop();
        SceneManager.LoadSceneAsync(LoadScene);

    }
}
