using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public class DataBase_quest : GetIRefreshing {
    static DataBase_quest dataBase_;
    public static int curent_QuestLevel;
    public int deb_curLevel;
    [SerializeField] List<MainQuest> mainQuest = new List<MainQuest> ();

    [SerializeField] List<SubQuest> subQuest = new List<SubQuest> ();

    public List<SubQuest_progress> subQuests_progress = new List<SubQuest_progress> ();

    private void Awake () {
        deb_curLevel = curent_QuestLevel;
        if (dataBase_ == null) {
            DontDestroyOnLoad (this.gameObject);
            dataBase_ = this;
        } else if (dataBase_ != this) {
            Destroy (this.gameObject);
        }

        curent_QuestLevel = 1;
    }

    private void Start () {
        LoadMainQuest ();
    }

    public static void AddSubQuest (SubQuest_progress subQ_data) {

        dataBase_.subQuests_progress.Add (subQ_data);
    }

    public static SubQuest_progress GenerateSubQuest () {
        int level = curent_QuestLevel < 3 ? curent_QuestLevel - 1 : 2;
        
        SubQuest_progress subQuest_data = new SubQuest_progress ();
        subQuest_data.MakeQuest (dataBase_.subQuest[level]);

        return subQuest_data;
    }

    public static MainQuest GetCurentMainQuest () {
        return dataBase_.mainQuest[curent_QuestLevel - 1];
    }
    public static SubQuest_progress GetSubQuestByID (int id) {
        foreach (SubQuest_progress item in dataBase_.subQuests_progress) {
            if (item.id == id) {
                return item;
            }
        }
        return null;
    }
    public static void ClearSubQuestbyID (int id) {
        dataBase_.subQuests_progress.Remove (GetSubQuestByID (id));
    }
    public static List<SubQuest_progress> GetCurentSubQuest () {
        return dataBase_.subQuests_progress;
    }

    public static void CheckSoldFish (Kolam_data data, int soldAmount) {
        foreach (SubQuest_progress item in dataBase_.subQuests_progress) {
            item.CheckSoldFish (data, soldAmount);
        }
    }

    public static void SaveQuestProgress () {
        BinaryFormatter bf = new BinaryFormatter ();

        FileStream file = File.Create (DataBase.savePath + "/subQ_dat.dta");

        List<SubQuest_progress> qData = new List<SubQuest_progress> ();
        qData = dataBase_.subQuests_progress;

        bf.Serialize (file, qData);
        file.Close ();
    }
    public static void LoadSubQuest () {
        if (File.Exists (DataBase.savePath + "/subQ_dat.dta")) {
            BinaryFormatter bf = new BinaryFormatter ();
            //FileStream file = File.Open(Application.persistentDataPath + "/levelData.sd", FileMode.Open); // 
            FileStream file = File.Open (DataBase.savePath + "/subQ_dat.dta", FileMode.Open);
            List<SubQuest_progress> data = (List<SubQuest_progress>) bf.Deserialize (file);
            file.Close ();

            foreach (SubQuest_progress item in data) {
                dataBase_.subQuests_progress.Add (item);
            }
        }
    }

    public static void SaveMainQuest () {
        BinaryFormatter bf = new BinaryFormatter ();

        FileStream file = File.Create (DataBase.savePath + "/mainQ_dat.dta");

        int qMData;
        qMData = curent_QuestLevel;

        bf.Serialize (file, qMData);
        file.Close ();
    }

    public static void LoadMainQuest () {
        if (File.Exists (DataBase.savePath + "/mainQ_dat.dta")) {
            BinaryFormatter bf = new BinaryFormatter ();
            //FileStream file = File.Open(Application.persistentDataPath + "/levelData.sd", FileMode.Open); // 
            FileStream file = File.Open (DataBase.savePath + "/mainQ_dat.dta", FileMode.Open);
            int data = (int) bf.Deserialize (file);
            file.Close ();

            curent_QuestLevel = data;
        }
    }
}