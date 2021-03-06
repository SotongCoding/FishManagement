using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName = "MainQuestAsset", fileName = "mainQuest")]
public class MainQuest : ScriptableObject {
    public bool isUnlock;
    public string tittle;
    [TextArea]
    public string dialouge;

    [Header ("Reward")]
    public int gold;
    public int exp;

    PlayerStatistik data;
    PlayerStatistik2 data2;
    public bool CheckRequairment () {
        data = DataBase.GetGamePlayData_PlayerData ();
        data2 = DataBase.GetGameStatistic ();

        int requestCheck = DataBase_quest.curent_QuestLevel;

        if (requestCheck == 1) {
            return (
                data.kolamUnlock >= 2 && // Buka 2 Kolam
                data.gold_get >= 1000000 && // Dapatkan uang sejumlah 1 juta
                data2.GetSellDataByFishType (type_ikanBudidya.leleLokal).SUM () >= 500 //Jual Ikan Lele Lokal sebanyak 500 ekor
            );
        } else if (requestCheck == 2) {
            return (
                data.kolamUnlock >= 3 && // Buka Kolam 3

                data2.GetSellDataByFishType (type_ikanBudidya.leleLokal).SUM () >= 1500 && // total Jual ikan Lokal sebanyak 1500 ekor
                data2.GetSellDataByFishType (type_ikanBudidya.leleJumbo).SUM () >= 600 && // total jual Ikan Dumbo 600 ekor

                data2.GetSellDataByFishType (type_ikanBudidya.leleLokal).GetJualQuality_andMore (70) >= 1000 // Total jual Lokal kualitas 70^ 1000
            );
        } else if (requestCheck == 3) {
            return (
                data.kolamUnlock >= 4 && // Buka Kolam 4

                data2.GetSellDataByFishType (type_ikanBudidya.leleLokal).GetJualQuality_andMore (70) >= 2000 && // Total jual Lokal kualitas 70^ 2000
                data2.GetSellDataByFishType (type_ikanBudidya.leleJumbo).GetJualQuality_andMore (70) >= 1000 && // Total jual Jumbo kualitas 70^ 1000
                data2.GetSellDataByFishType (type_ikanBudidya.leleSangkuriang).GetJualQuality_andMore (70) >= 500 && // Total jual sangkur kualitas 70^ 500

                data2.GetPanenDataByFishType (type_ikanBudidya.leleLokal).SUM () >= 3000 && // total panen Lokal 6000
                data2.GetPanenDataByFishType (type_ikanBudidya.leleJumbo).SUM () >= 2500 && // total panen jumbo 5000
                data2.GetPanenDataByFishType (type_ikanBudidya.leleSangkuriang).SUM () >= 1500 // total panen sangkur 4000

            );
        } else if (requestCheck == 4) {
            return (
                data.gold_curent >= 1000000000
            );
        } else {
            return false;
        }

    }

    public void GetReward () {

        DataBase.GetGamePlayData_PlayerData ().changeGold (+gold);
        DataBase.GetGamePlayData_PlayerData ().increaseEXP (+exp);

        if (DataBase_quest.curent_QuestLevel == 1) {
            DataBase.getShopDatabyId (22).unlock = true;
        } else if (DataBase_quest.curent_QuestLevel == 2) {
            DataBase.getShopDatabyId (23).unlock = true;
        }

        DataBase_quest.curent_QuestLevel++;
        Debug.LogWarning (DataBase_quest.curent_QuestLevel);
        DataBase_quest.SaveMainQuest ();
        DataBase.SaveProgress ();
    }
}