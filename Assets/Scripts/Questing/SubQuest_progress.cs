using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubQuest_progress {

    public int id;
    public string tittle;

    [Header ("Requaire")]
    public type_ikanBudidya jenisIkan;
    public int kebutuhanIkan;
    public int qualityIkan;
    public float beratIkan;

    [Header ("Reward")]
    public int gold;
    public int exp;

    [Header ("Curent Progress")]
    public int cur_kebutuhanIkan;

    [Header ("NPC Data")]

    public NPCData npc;

    public void MakeQuest (SubQuest data) {
        int index_requ = Random.Range (0, data.require.Count);

        int randomButuh = Random.Range (data.require[index_requ].butuhMin, data.require[index_requ].butuhMax);
        int randomQuality = Random.Range (data.require[index_requ].qualityMin, data.require[index_requ].qualityMax);
        float randomBerat = Random.Range (data.require[index_requ].wightMin, data.require[index_requ].weightMax);

        int randomGold = Random.Range (data.goldMin, data.goldMax);
        int randomExp = Random.Range (data.expMin, data.expMax);

        //==========

        jenisIkan = data.require[index_requ].jenisIkan;
        kebutuhanIkan = randomButuh;
        qualityIkan = randomQuality;
        beratIkan = randomBerat;
        //---------
        id = Random.Range (1, 101);
        string ikan = "Lele";

        if (jenisIkan == type_ikanBudidya.leleLokal) {
            ikan = "Lele Lokal";
        } else if (jenisIkan == type_ikanBudidya.leleJumbo) {
            ikan = "Lele Jumbo";
        } else if (jenisIkan == type_ikanBudidya.leleSangkuriang) {
            ikan = "Lele Sangkuraiang";
        }
        
        tittle = "Pesanan " + ikan + " ";
        for (int i = 0; i < data.questLevel; i++) {
            tittle += "I";
        }

        gold = randomGold;
        exp = randomExp;
    }
    public bool CheckRequairment () {
        return cur_kebutuhanIkan >= kebutuhanIkan;
    }

    public void GetReward () {
        DataBase.GetGamePlayData_PlayerData ().changeGold (+gold);
    }

    public void CheckSoldFish (Kolam_data data, int amountSoldFish) {
        if (data.dataIkan.jenisIkan == jenisIkan && data.dataIkan.kualitas >= qualityIkan && data.dataIkan.berat >= beratIkan) {
            cur_kebutuhanIkan += amountSoldFish;
        }
    }
}