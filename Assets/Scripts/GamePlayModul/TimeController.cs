using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {
    static TimeController timeControl;
    public static bool timeOn = false;
    float curTime;
    int realSecond;
    bool questionerOn;
    bool showThisAgain;
    bool ev1 = false, ev2 = false, ev3 = false;

    public TimeGame timeOnGame;
    [SerializeField]
    public List<GetIRefreshing> refreshClass = new List<GetIRefreshing> ();

    //Times Group
    public int counter = 0;

    private void Awake () {
        if (timeControl == null) {
            refreshClass = FindObjectsOfType<GetIRefreshing> ().ToList ();
            DontDestroyOnLoad (this.gameObject);
            timeControl = this;
        } else if (timeControl != this) {
            Destroy (this.gameObject);
        }
        TimeController.timeOn = false;
        showThisAgain = true;
        questionerOn = timeOnGame.questionerOnce;

    }

    public static TimeGame GetTime () {
        return timeControl.timeOnGame;
    }
    public static void SetTime (TimeGame time) {
        timeControl.timeOnGame = time;
        timeControl.counter = (int) timeControl.timeOnGame.hour;

        timeControl.showThisAgain = true;
        timeControl.questionerOn = time.questionerOnce;
    }
    private void LateUpdate () {
        timeOnGame.realSecond += Time.deltaTime;
        if (timeOnGame.realSecond >= 1800 && !timeOnGame.questionerOnce) {
            questionerOn = true;
            timeOnGame.questionerOnce = true;
            DennyMessage.ShowNotifQuestioner ();
        }

        if (timeOn) {
            curTime += (Time.deltaTime);
            if (curTime > 6f) {
                curTime = 0;
                counter++;
                ev1 = true;
                ev2 = true;
                ev3 = true;
                timeOnGame.hour = counter;

                DataBase.SaveProgress ();

                if (timeOnGame.hour > 12) {
                    timeOnGame.hour = 1;
                    timeControl.counter = (int) timeControl.timeOnGame.hour;
                }

            }
        }
        if (counter % 4 == 0 && ev1) {
            doRulesKolam ();
            ev1 = false;
        }
        if (counter % 2 == 0 && ev2) {
            doRulesIkan ();

            ev2 = false;
        }
        if (counter % 12 == 0 && ev3) {
            doRulesWeek ();
            timeOnGame.hour = 12;
            counter = 0;
            ev3 = false;
            if (questionerOn && showThisAgain) {
                DennyMessage.ShowNotifQuestioner ();
                questionerOn = false;

            }
        }

    }

    void doRulesKolam () {
        foreach (GetIRefreshing refres in refreshClass) {
            refres.UpdatingValue (type_time.update_data_kolam);
        }
    }
    void doRulesIkan () {
        foreach (GetIRefreshing refres in refreshClass) {
            refres.UpdatingValue (type_time.update_data_ikan);
        }
    }

    void doRulesWeek () {
        foreach (GetIRefreshing refres in refreshClass) {
            refres.UpdatingValue (type_time.endTime);
        }
        timeOnGame.week++;
        timeOnGame.month = timeOnGame.week / 4;
    }

    public void OpenLink () {
        Application.OpenURL ("https://forms.gle/JC6gZY5oA1Ap5CkVA");
    }
    public void Later () {
        DennyMessage.CloseNotifQuestioner ();
        questionerOn = true;
    }
    public void NeverOpen () {
        showThisAgain = false;
        DennyMessage.CloseNotifQuestioner ();
    }
}

[System.Serializable]
public class TimeGame {
    //public float minutes;

    public float realSecond;
    public float hour;
    public int week;

    public int month;

    public bool questionerOnce = false;
}