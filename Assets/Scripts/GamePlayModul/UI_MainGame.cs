using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MainGame : MonoBehaviour {
    public GameObject helpObject;
    public GameObject statistikObj;
    //Status
    PlayerStatistik playerData;
    public Text gold;
    public Text level;
    public Text cur_exp;
    public Image fillExp;
    public void OpenShop () {
        if (SceneManager.GetSceneByName ("Shop").isLoaded) {
            SceneManager.UnloadSceneAsync ("Shop");
        } else {
            SceneManager.LoadScene ("Shop", LoadSceneMode.Additive);
        }

        SoundControl.playSoundFX (SoundType.klik);
    }
    public void OpenGalangan () {
        SceneManager.LoadScene ("Galangan");
    }
    public void OpenHelp () {
        helpObject.SetActive (!helpObject.activeSelf);
        SoundControl.playSoundFX (SoundType.klik);
    }
    public void OpenDetailObject (GameObject detailObject) {
        detailObject.SetActive (!detailObject.activeSelf);
        Debug.Log ("This");
        SoundControl.playSoundFX (SoundType.klik);
    }
    public void OpenStatistik () {
        statistikObj.SetActive (true);
    }

    private void Update () {
        playerData = DataBase.GetGamePlayData_PlayerData ();

        gold.text = Enums.ConverterNumber (playerData.gold_curent);
        level.text = playerData.level.ToString ();

        int val = playerData.level > 0 ? playerData.reqEXP[playerData.level - 1] : 0;
        cur_exp.text = (playerData.curEXP - val).ToString ();

        fillExp.fillAmount = (float) playerData.curEXP / (float) playerData.reqEXP[playerData.level];
    }

    private void Start () {
        TutorControl.ShowTutor (0);
    }

    public void ShowOption () {
        FindObjectOfType<StartMenuUI> ().OptionMenu ();
    }

}