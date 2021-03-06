using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour {

    public GameObject exitObj;
    public GameObject optionObj;
    public GameObject aboutObj;
    public RectTransform NPC;

    bool isQuiting = false;

    private void Awake () {
        isQuiting = false;
        SceneManager.LoadScene ("Loading", LoadSceneMode.Additive);
    }
    public void GoToMainGame () {
        LoadingHandler.LoadSceneWithLoading ("Home2");
        // SceneManager.LoadScene ("Home2");
        TimeController.timeOn = true;
        SoundControl.playSoundFX (SoundType.klik);

        if (DataBase.GetGamePlayData_PlayerData ().isPlayed == 0) {
            DataBase.GetGamePlayData_PlayerData ().isPlayed = 1;

            DataBase.SaveProgress ();
        }
    }

    private void Update () {
        if (Input.GetKeyDown (KeyCode.Escape)) {
            exitObj.SetActive (!exitObj.activeSelf);
        }
        if (isQuiting) {
#if UNITY_WEBGL

#else
            Application.Quit ();
#endif
        }
    }

    public void ShowExitMenu () {
        exitObj.SetActive (!exitObj.activeSelf);
    }

    public void ExitButtons (bool value) {
        if (value) {
            DataBase.SaveProgress ();
            DataBase_quest.SaveMainQuest ();
            DataBase_quest.SaveQuestProgress ();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

#if UNITY_ANDROID
            isQuiting = true;
#endif
        } else {
            exitObj.SetActive (false);
        }
    }
    public void OptionMenu () {
        optionObj.SetActive (!optionObj.activeSelf);
    }

    public void OpenAbout () {
        aboutObj.SetActive (!aboutObj.activeSelf);
    }
}