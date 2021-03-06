using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingHandler : MonoBehaviour {

    public GameObject loadObject;
    static LoadingHandler loading;
    private void Awake () {
        if (loading == null) {
            DontDestroyOnLoad (this.gameObject);
            loading = this;
        } else if (loading != this) {
            Destroy (this.gameObject);
        }
    }
    public static void LoadSceneWithLoading (string sceneName) {
        LeanTween.alpha (loading.loadObject.GetComponent<RectTransform> (), 1, 0.5f);
        loading.StartCoroutine (loading.BeginLoading (sceneName));
    }
    public static void LoadSceneWithLoading (string sceneName, LoadSceneMode mode) {
        LeanTween.alpha (loading.loadObject.GetComponent<RectTransform> (), 1, 0.5f);
        loading.StartCoroutine (loading.BeginLoading (sceneName, mode));
    }

    IEnumerator LoadingStart (string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (sceneName);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone) {
            yield return new WaitForEndOfFrame ();
        }
        LeanTween.alpha (loadObject.GetComponent<RectTransform> (), 0, 0.5f);

    }
    IEnumerator LoadingStart (string sceneName, LoadSceneMode mode) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (sceneName, mode);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone) {
            yield return new WaitForSeconds (2);

        }

        LeanTween.alpha (loadObject.GetComponent<RectTransform> (), 0, 0.5f);

    }

    IEnumerator BeginLoading (string sceneName) {
        yield return new WaitForSeconds (0.5f);
        StartCoroutine (LoadingStart (sceneName));
    }
    IEnumerator BeginLoading (string sceneName, LoadSceneMode mode) {
        yield return new WaitForSeconds (0.5f);
        StartCoroutine (LoadingStart (sceneName, mode));
    }
}