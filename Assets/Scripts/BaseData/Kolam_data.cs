using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Kolam_data {
    public bool unlock;
    public bool feedAble;
    public bool canUse;
    public int numberFeed;
    //[SerializeField] private PoolTimer timer;
    public Kolam dataKolam;
    public Ikan_ChangeAble dataIkan;

    public void UpdatingValueFish () {
        if (dataKolam.jumlah_ikan <= 0) {
            dataIkan.jenisIkan = type_ikanBudidya.empty;
        }
        //Kesehatan Ikan
        float fromOksigen, fromPH, fromKebersihan, fromLapar, fromPenyakit;

        fromOksigen = dataKolam.oksigen <= 3000 ? 7 : 0;
        fromPH = dataKolam.ph > 8 || dataKolam.ph < 6.5f ? 5 : 0;
        fromKebersihan = dataKolam.kebersihan < 30 ? 3 : -2;

        if (dataKolam.kebersihan < 20) {
            dataKolam.chance_penyakit += 0.3f * (dataKolam.jumlah_ikan / 10);
        } else if (dataKolam.kebersihan > 80) {

            if (dataKolam.chance_penyakit > 0) dataKolam.chance_penyakit -= 0.1f * (dataKolam.jumlah_ikan / 10);

            else dataKolam.chance_penyakit = 0;
        }

        fromLapar = dataIkan.lapar_durability < (0.2f * dataKolam.beratMasa) ? 5 : -4;
        fromPenyakit = dataKolam.penyakit != type_penyakit.empty? 10 : 0;

        //Calculating Health
        dataIkan.health -= (fromOksigen + fromPH + fromKebersihan + fromLapar + fromPenyakit);

        if (dataIkan.health > 100) {
            dataIkan.health = 100;
        } else if (dataIkan.health < 0) {
            dataIkan.health = 0;
        }

        //Determine Fish Death
        dataKolam.getFishDeath (dataIkan);
        //===========================================================
        dataIkan.lapar_durability -= (0.35f * dataKolam.beratMasa);

        if (dataIkan.lapar_durability < 0) {
            dataIkan.lapar_durability = 0;
        }
        feedAble = dataIkan.lapar_durability <= (0.55f * dataKolam.beratMasa) ? true : false;

        if (dataKolam.penyakit == type_penyakit.empty) {
            dataKolam.GetPenyakit ();
        } else if (dataKolam.penyakit != type_penyakit.empty && dataIkan.penyakit_durability <= 0) {
            dataKolam.penyakit = type_penyakit.empty;
        }

        //Determine Kualitas dari Ikan

        dataIkan.kualitas =
            ((dataIkan.berat / 100) * 0.4f) * 100 +
            ((dataIkan.health / 100) * 0.6f) * 100;

        //Other data
        dataIkan.GetCurentSize ();
        UpdatingOther ();
    }

    public void UpdatingValuePool () {
        float oksigen, ph, kebersihan;

        oksigen = (dataKolam.beratMasa / 50);
        ph = (dataKolam.jumlah_ikan / 60) * 0.03f;
        kebersihan = dataKolam.jumlah_ikan * 0.025f;

        dataKolam.oksigen -= oksigen;
        dataKolam.ph -= ph;
        dataKolam.kebersihan -= kebersihan;

        UpdatingOther ();
    }

    public void UpdatingValueLast () {
        dataIkan.berat += dataIkan.GetGrowthRateValue ();
        dataKolam.beratMasa = dataIkan.berat * dataKolam.jumlah_ikan;
        UpdatingOther ();
    }

    public void UpdatingOther () {
        if (dataKolam.kebersihan < 0) dataKolam.kebersihan = 0;

        if (dataKolam.oksigen < 0) dataKolam.oksigen = 0;

        if (dataKolam.ph < 0) dataKolam.ph = 0;
        else if (dataKolam.ph > 14) dataKolam.ph = 14;

        if (dataIkan.kualitas > 100) {
            dataIkan.kualitas = 100;
        }

        //kolamONplay.CheckStatus ();
    }
}