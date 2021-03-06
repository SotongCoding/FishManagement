using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ikan_ChangeAble {
    public int id;
    public type_ikanBudidya jenisIkan;
    public type_ikansize ukuran;
    public float health;
    public float berat;
    public float umur;
    public float kualitas;
    public float penggunaan_pakan;
    public float ori_growth_rate;
    public float cur_growth_rate;

    public float penyakit_durability;
    public float lapar_durability;

    public int jualDasar;

    List<float> inputGrowthValue = new List<float> ();

    [Header ("Data Panen")]
    public float panen_size;

    public bool CheckPanen () {
        return berat == panen_size;
    }

    public Ikan_ChangeAble (Ikan data) {
        id = data.id;

        jenisIkan = data.jenisIkan;
        health = data.health;
        berat = data.berat;
        umur = data.umur;

        kualitas = data.kualitas;
        penggunaan_pakan = data.penggunaan_pakan;
        ori_growth_rate = data.growth_rate;
        ukuran = data.ukuran;

        penyakit_durability = data.penyakit_durability;
        lapar_durability = data.lapar_durability;

        jualDasar = data.jualDasar;
    }
    public Ikan_ChangeAble () {
        id = 0;

        jenisIkan = type_ikanBudidya.empty;
        health = 0;
        berat = 0;
        umur = 0;

        kualitas = 0;
        penggunaan_pakan = 0;
        ori_growth_rate = 0;
        ukuran = 0;

        penyakit_durability = 0;
        lapar_durability = 0;

        jualDasar = 0;
    }

    public void GetCurentSize () {
        if (berat < 50) ukuran = type_ikansize.kecil;
        else if (berat >= 50 && berat <= 80) ukuran = type_ikansize.sedang;
        else if (berat > 80) ukuran = type_ikansize.besar;
    }
    public void CalculatingGrowthRate (float inputValue) {
        inputGrowthValue.Add (inputValue);
        float sum = 0;
        if (inputGrowthValue.Count > 0) {
            foreach (float item in inputGrowthValue) {
                sum += item;
            }

            cur_growth_rate = sum / inputGrowthValue.Count;
        }
    }
    public float GetGrowthRateValue () {
        inputGrowthValue.Clear ();

        return cur_growth_rate;
    }
}