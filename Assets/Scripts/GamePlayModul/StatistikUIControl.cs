using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatistikUIControl : MonoBehaviour {

    GameObject curObject;
    //Data Budidaya
    [Header ("Panen")]
    public Text panen_total;
    public Text panen_b50, panen_b100, panen_b200;
    public Text panen_k50, panen_k70, panen_k90;

    [Header ("Jual")]
    public Text jual_total;
    public Text jual_b50, jual_b100, jual_b200;
    public Text jual_k50, jual_k70, jual_k90;

    //Data Play game
    [Header ("GamePlay")]
    public Text waktu;
    public Text goldGain, goldUse;

    public void ShowObject (GameObject target) {
        target.SetActive (!target.activeSelf);
    }
    public void InitialData (string jenisIkan) {
        Statik_PanenData panen = new Statik_PanenData ();
        Statik_SellData jual = new Statik_SellData ();

        type_ikanBudidya jenis = (type_ikanBudidya) System.Enum.Parse (typeof (type_ikanBudidya), jenisIkan);

        panen = DataBase.GetGameStatistic ().GetPanenDataByFishType (jenis);

        jual = DataBase.GetGameStatistic ().GetSellDataByFishType (jenis);

        jual_total.text = jual.SUM ().ToString () +" ekor";

        jual_b50.text = jual.jual_berat_50.ToString ();
        jual_b100.text = jual.jual_berat_100.ToString ();
        jual_b200.text = jual.jual_berat_200.ToString ();

        jual_k50.text = jual.jual_kualitas_50.ToString ();
        jual_k70.text = jual.jual_kualitas_70.ToString ();
        jual_k90.text = jual.jual_kualitas_90.ToString ();
        //==================================================
        panen_total.text = panen.SUM ().ToString () + " ekor";

        panen_b50.text = panen.panen_berat_50.ToString ();
        panen_b100.text = panen.panen_berat_100.ToString ();
        panen_b200.text = panen.panen_berat_200.ToString ();

        panen_k50.text = panen.panen_kualitas_50.ToString ();
        panen_k70.text = panen.panen_kualitas_70.ToString ();
        panen_k90.text = panen.panen_kualitas_90.ToString ();
    }

    public void InitialDataGamePlay () {
        TimeGame time = new TimeGame ();
        time = DataBase.GetGamePlayData_GameTime ();

        int second, minutes, hour;
        second = (int) time.realSecond;
        minutes = second / 60 % 60;
        hour = second / 3600;

        waktu.text = "JAM : " + hour + " MENIT : " + minutes + " DETIK : " + (second % 60);

        goldGain.text = DataBase.GetGamePlayData_PlayerData ().gold_get.ToString () +
            "(" + Enums.ConverterNumber (DataBase.GetGamePlayData_PlayerData ().gold_get) + ")";

        goldUse.text = DataBase.GetGamePlayData_PlayerData ().gold_use.ToString () +
            "(" + Enums.ConverterNumber (DataBase.GetGamePlayData_PlayerData ().gold_use) + ")";
    }
}