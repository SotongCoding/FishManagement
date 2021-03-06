using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_BudidayaKontrol : MonoBehaviour {
    //UI DATA KOLAM
    SpwanIkan spawnIkanControl;
    public Kolam_data dataKolam;

    [Header ("Inventory")]
    public Text amount_pakan1;
    public Text amount_pakan2, amount_pakan3;
    public Text amount_obat1, amount_obat2, amount_obat3;
    public Text amount_pepaya, amount_kapur;

    [Header ("Indikator Kolam")]
    public Text kebersihanKolam_text;
    public Text tingkat_oksigen, tingkat_pH;
    public Image poolFish_icon;

    [Header ("Indikator Ikan")]
    public Text kualitas_ikan_text;
    public Text size_text, kesehatan_text;
    public Text size_category, amount;
    public Image hungry;
    public Image sick;

    [Header ("Prepare")]
    public GameObject prepareObject;
    public GameObject fillObject;
    public Image prepareFill;
    bool prepareLoading = false;
    float curLoadingAmount = 0;

    private void Start () {
        spawnIkanControl = FindObjectOfType<SpwanIkan> ();
        dataKolam = FindObjectOfType<WorldManager> ().getDataKolam ();

        if (!dataKolam.canUse && dataKolam.dataKolam.ikan == type_ikanBudidya.empty) {
            prepareObject.SetActive (true);

        } else {
            prepareObject.SetActive (false);
            spawnIkanControl.CreateFish (dataKolam.dataKolam.jumlah_ikan, dataKolam.dataKolam.ikan);
        }

    }

    private void Update () {
        if (prepareLoading) {
            if (prepareFill.fillAmount >= 1) {

                //DO prepare
                DoPrepare ();
                prepareObject.SetActive (false);

                prepareLoading = false;
            } else {
                curLoadingAmount += Time.deltaTime * 30;
                prepareFill.fillAmount = curLoadingAmount / 100;
            }
        }
        InitializeData ();
    }

    void InitializeData () {
        //Indikator Inventory
        amount_pakan1.text = (DataBase.GetCurrentPakan (pakan_type.p30_40) / 1000).ToString ("0.00") + " kg";
        amount_pakan2.text = (DataBase.GetCurrentPakan (pakan_type.p41_60) / 1000).ToString ("0.00") + " kg";
        amount_pakan3.text = (DataBase.GetCurrentPakan (pakan_type.p61_80) / 1000).ToString ("0.00") + " kg";

        amount_obat1.text = (DataBase.GetObatbyID (11).jumlah_obat).ToString();
        amount_obat2.text = (DataBase.GetObatbyID (12).jumlah_obat).ToString();
        amount_obat3.text = (DataBase.GetObatbyID (13).jumlah_obat).ToString ();

        amount_pepaya.text = (DataBase.GetObatbyID (21).jumlah_obat).ToString ();
        amount_kapur.text = (DataBase.GetObatbyID (22).jumlah_obat).ToString ();

        //Indikator Kolam
        kebersihanKolam_text.text = dataKolam.dataKolam.kebersihan.ToString ("0.00");
        tingkat_oksigen.text = (dataKolam.dataKolam.oksigen / 1000).ToString ("0.00") + " mg/L";
        tingkat_pH.text = dataKolam.dataKolam.ph.ToString ("0.00");

        poolFish_icon.sprite =
            SpriteLoader.LoadSpriteFish (dataKolam.dataKolam.ikan.ToString ());

        //Indikator Ikan

        if (dataKolam.dataIkan.jenisIkan != type_ikanBudidya.empty) {

            kualitas_ikan_text.text = ((int) dataKolam.dataIkan.kualitas).ToString ();
            size_text.text = dataKolam.dataIkan.berat.ToString ("0.00") + "g";
            kesehatan_text.text = dataKolam.dataIkan.health.ToString ();

            hungry.fillAmount = dataKolam.dataIkan.lapar_durability / dataKolam.dataKolam.beratMasa;
            sick.fillAmount = dataKolam.dataIkan.penyakit_durability / 100;

            size_category.text = dataKolam.dataIkan.ukuran.ToString ().ToUpper ();
            amount.text = dataKolam.dataKolam.jumlah_ikan.ToString ();
        }
    }

    public void close_kolamUI () {

        if (SceneManager.GetSceneByName ("Kolam").isLoaded) {
            TutorControl.ShowTutor (2);
            SceneManager.UnloadSceneAsync ("Kolam");
        }
    }

    public void PrepareLoading () {
        if (prepareLoading) {
            prepareLoading = false;

            prepareFill.fillAmount = 1;
            curLoadingAmount = 0;
        } else {
            prepareLoading = true;
            prepareFill.fillAmount = 0;
        }
        SoundControl.playSoundFX (SoundType.klik);
    }

    public void OpenResetKolam () {
        prepareObject.SetActive (true);
        prepareLoading = false;

        kualitas_ikan_text.text = ((int) dataKolam.dataIkan.kualitas).ToString ();
        size_text.text = dataKolam.dataIkan.berat.ToString ("0.00") + "g";
        kesehatan_text.text = dataKolam.dataIkan.health.ToString ();

        hungry.fillAmount = dataKolam.dataIkan.lapar_durability / dataKolam.dataKolam.beratMasa;
        sick.fillAmount = dataKolam.dataIkan.penyakit_durability / 100;

        size_category.text = dataKolam.dataIkan.ukuran.ToString ().ToUpper ();
        amount.text = dataKolam.dataKolam.jumlah_ikan.ToString ();

        kebersihanKolam_text.text = dataKolam.dataKolam.kebersihan.ToString ("0.00");
        tingkat_oksigen.text = (dataKolam.dataKolam.oksigen / 1000).ToString ("0.00") + " mg/L";
        tingkat_pH.text = dataKolam.dataKolam.ph.ToString ("0.00");
    }
    void DoPrepare () {
        dataKolam.dataKolam.kebersihan = 100;
        dataKolam.dataKolam.ph = 7;
        dataKolam.dataKolam.oksigen = 5000;

        dataKolam.dataKolam.air = type_air.tawar;
        dataKolam.canUse = true;

        TutorControl.ShowTutor (8);
    }

    public void UpdateUI () {
        //Kolam
        kebersihanKolam_text.text = dataKolam.dataKolam.kebersihan.ToString ();
        tingkat_oksigen.text = dataKolam.dataKolam.oksigen.ToString ();
        tingkat_pH.text = dataKolam.dataKolam.ph.ToString ();

        //Ikan
        kualitas_ikan_text.text = dataKolam.dataIkan.kualitas.ToString ();

        size_text.text = dataKolam.dataIkan.berat.ToString ();
        kesehatan_text.text = dataKolam.dataIkan.health.ToString ();
    }

}