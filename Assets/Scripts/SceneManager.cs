using System.Collections;
using UnityEditor;
using UnityEngine;
using SM = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    private float delayTime;
    private bool waitingForSceneChange;

    public void SetSceneChangeDelay(float delay) {
        delayTime = delay;
    }

    public void ChangeScene(SceneAsset scene) {
        if (waitingForSceneChange) {
            Debug.LogWarning($"Attempting to load scene {scene.name} while another scene is already being loaded. " +
                             $"{scene.name} will not be loaded.");
            return;
        }

        if (delayTime > 0) {
            StartCoroutine(ChangeSceneDelay(scene, delayTime));
        }
        else SM.LoadScene(scene.name);
    }

    private IEnumerator ChangeSceneDelay(SceneAsset scene, float time) {
        Debug.Log("starting coroutine");
        waitingForSceneChange = true;
        yield return new WaitForSecondsRealtime(time);
        waitingForSceneChange = false;
        delayTime = 0;
        Debug.Log("ending coroutine");
        SM.LoadScene(scene.name);
    }
}