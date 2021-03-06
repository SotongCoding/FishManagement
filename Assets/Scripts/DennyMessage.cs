using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DennyMessage : MonoBehaviour {
    public GameObject notif;
    public GameObject notifQuestioner;
    public Text notif_text;
    public static DennyMessage notifControl;
    bool isShow;

    private void Awake () {
        if (notifControl == null) {
            DontDestroyOnLoad (this.gameObject);
            notifControl = this;
        } else if (notifControl != this) {
            Destroy (this.gameObject);
        }
        isShow = false;
    }
    private void OnEnable () {

    }

    public static void ShowNotif (string message) {
        if (!notifControl.isShow) {
            LeanTween.moveLocalY (notifControl.notif, -10, 0.5f);
            notifControl.notif_text.text = message;
            notifControl.showObject ();
            notifControl.isShow = true;
        }

    }

    void showObject () {
        notifControl.notif.SetActive (true);
        StartCoroutine (BackToOrigin (5));
    }

    IEnumerator BackToOrigin (float time) {

        yield return new WaitForSeconds (time);
        LeanTween.moveLocalY (notif, 100, 0.5f);
        StartCoroutine (CloseNotif ());
    }
    IEnumerator CloseNotif () {

        yield return new WaitForSeconds (1);
        notif.SetActive (false);
        isShow = false;
    }
    public static void CloseNotifQuestioner () {
        LeanTween.moveLocalY (notifControl.notifQuestioner, 50, 0.5f);
    }

    public static void ShowNotifQuestioner () {
        LeanTween.moveLocalY (notifControl.notifQuestioner, -300, 0.5f);
    }

}