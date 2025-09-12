using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using SM = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    private float delayTime;
    private bool waitingForSceneChange;

    public void SetSceneChangeDelay(float delay) {
        delayTime = delay;
    }

    public void ChangeScene(string sceneName) {
        if (waitingForSceneChange) {
            Debug.LogWarning($"Attempting to load scene {sceneName} while another scene is already being loaded. " +
                             $"{sceneName} will not be loaded.");
            return;
        }

        if (delayTime > 0) {
            StartCoroutine(ChangeSceneDelay(sceneName, delayTime));
        }
        else SM.LoadScene(sceneName);
    }

    private IEnumerator ChangeSceneDelay(string sceneName, float time) {
        waitingForSceneChange = true;
        yield return new WaitForSecondsRealtime(time);
        waitingForSceneChange = false;
        delayTime = 0;
        SM.LoadScene(sceneName);
    }
}