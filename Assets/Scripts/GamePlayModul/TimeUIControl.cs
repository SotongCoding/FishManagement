using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUIControl : MonoBehaviour {
    public Image fillTime;

    public Text month, week;
    float fixValue, curValue = 0;

    private void Update () {

        fixValue = TimeController.GetTime ().hour / 12;
        curValue = Mathf.Lerp (curValue, fixValue, Time.deltaTime * 5);

        fillTime.fillAmount = curValue;

        month.text = TimeController.GetTime ().month.ToString ();
        week.text = (TimeController.GetTime ().week % 4).ToString ();
    }
}