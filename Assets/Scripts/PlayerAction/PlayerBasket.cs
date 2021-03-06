using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerBasket : MonoBehaviour {
    public BasketData basket;

    private void Start () {
        LoadProgress ();
    }

    public void SaveProgress () { 
        BinaryFormatter bf = new BinaryFormatter ();

        FileStream file = File.Create (DataBase.savePath + "/playerBas_dat.dta");

        BasketData saveBasket = new BasketData () {
            isContain = basket.isContain,

            jenisIkan = basket.jenisIkan,
            jumlahIkan = basket.jumlahIkan,
            beratIkan = basket.beratIkan,
            kualitasIkan = basket.kualitasIkan,
            basePrice = basket.basePrice
        };

        bf.Serialize (file, saveBasket);
        file.Close ();
    }
    public void LoadProgress () {
        if (File.Exists (DataBase.savePath + "/playerBas_dat.dta")) {
            BinaryFormatter bf = new BinaryFormatter ();
            //FileStream file = File.Open(Application.persistentDataPath + "/levelData.sd", FileMode.Open); // 
            FileStream file = File.Open (DataBase.savePath + "/playerBas_dat.dta", FileMode.Open);
            BasketData data = (BasketData) bf.Deserialize (file);
            file.Close ();

            basket = data;
        }
    }
}

[System.Serializable]
public class BasketData {
    public bool isContain;
    public bool onTrash;
    [Header ("From Kolam")]
    public type_ikanBudidya jenisIkan;
    public int jumlahIkan;
    public float beratIkan;
    public float kualitasIkan;
    public float basePrice;

    //=======================
    [Header ("From NPC")]
    public type_ikanBudidya req_jenisIkan;
    public float req_berat;
    public float req_kualitas;
    public int req_jumlahIkan;

    public void InitialStatusFish (Ikan_ChangeAble ikanData, int jumlahIkan) {
        jenisIkan = ikanData.jenisIkan;

        this.jumlahIkan = jumlahIkan;
        beratIkan = ikanData.berat;
        kualitasIkan = ikanData.kualitas;
        basePrice = ikanData.jualDasar;

        isContain = true;
    }
    public void InitialStatusSell (SubQuest_progress jualData, bool isOnTrash) {
        req_jenisIkan = jualData.jenisIkan;

        req_berat = jualData.beratIkan;
        req_kualitas = jualData.qualityIkan;
        req_jumlahIkan = jualData.kebutuhanIkan;
        onTrash = isOnTrash;
    }

    void ResetStatusSell () {
        req_jenisIkan = type_ikanBudidya.empty;

        req_berat = 0;
        req_kualitas = 0;
        req_jumlahIkan = 0;
        onTrash = false;
    }

    void ResetStatusFish () {
        jenisIkan = type_ikanBudidya.empty;

        this.jumlahIkan = 0;
        beratIkan = 0;
        kualitasIkan = 0;
        basePrice = 0;

        isContain = false;
    }

    int getSellPrice () {
        float base_berat = beratIkan > 100 ? 1 : beratIkan / 100;
        float val_berat = (base_berat * (0.4f * basePrice)) + ((beratIkan - req_berat) * 50);
        float val_kualitas = (0.6f * basePrice) + ((kualitasIkan - req_kualitas) * 70);

        return (int) ((val_berat + val_kualitas) * jumlahIkan);
    }
    public bool CheckSellFish () {
        if (onTrash) {
            return true;
        } else {
            return jenisIkan == req_jenisIkan && jumlahIkan >= req_jumlahIkan &&
                beratIkan >= req_berat && kualitasIkan >= req_kualitas;
        }
    }
    public void SoldFish (bool onClearingNPC) {
        if (CheckSellFish ()) {
            DataBase.GetGamePlayData_PlayerData ().changeGold (
                onClearingNPC ? +(getSellPrice () / 2) : +getSellPrice ());

            jumlahIkan -= req_jumlahIkan;

            if (jumlahIkan <= 0) {
                ResetStatusFish ();
            }
            ResetStatusSell ();
        }
    }
}