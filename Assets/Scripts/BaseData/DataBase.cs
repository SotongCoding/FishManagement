using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataBase : GetIRefreshing {
    static DataBase dataBase;
    [SerializeField] public List<Ikan_data> dataIkan;
    static List<Kolam_data> dataKolam;
    //---------------------------------------------------------------------------
    static otherBuildingData dataBangunan;
    static List<ItemShop> dataShop;

    static GamePlayData dataGamePlay;
    static PlayerStatistik2 dataStatistik;
    public static string savePath;
    //---------------------------------------------------------------------------

    //===========================================================================
    [Header ("First Data")]
    //===========================================================================
    [SerializeField] public List<Kolam_data> first_dataKolam;
    public otherBuildingData first_dataBangunan;
    [SerializeField] public List<ItemShop> first_dataShop;
    public GamePlayData first_dataGamePlay;
    public PlayerStatistik2 first_statistik;
    //============================================================================

    //============================================================================
    [Header ("Debugging Data")]
    //============================================================================

    [SerializeField] public List<Kolam_data> debug_dataKolam;
    public otherBuildingData debug_dataBangunan;
    [SerializeField] public List<ItemShop> debug_dataShop;
    public GamePlayData debug_dataGamePlay;
    public PlayerStatistik2 debug_statistik;

    private void Awake () {
        if (dataBase == null) {
            DontDestroyOnLoad (this.gameObject);
            dataBase = this;
        } else if (dataBase != this) {
            Destroy (this.gameObject);
        }

#if UNITY_ANDROID
        savePath = Application.persistentDataPath;
#elif UNITY_EDITOR
        savePath = Application.dataPath;
#elif UNITY_STANDALONE
     savePath = Application.persistentDataPath;
#endif
    }
    private void Start () {

        dataKolam = first_dataKolam;
        dataShop = first_dataShop;
        dataBangunan = first_dataBangunan;
        dataGamePlay = first_dataGamePlay;
        dataStatistik = first_statistik;

        debug_dataShop = dataShop;
        debug_dataKolam = dataKolam;
        debug_dataBangunan = dataBangunan;
        debug_dataGamePlay = dataGamePlay;
        debug_statistik = dataStatistik;

        LoadProgress ();

    }
    //General SaveLoad()
    public static void SaveProgress () {
        SaveDataGame ();
        SaveDataGudang ();
        SaveDataKolam ();
        SaveDataShop ();
    }

    public static void LoadProgress () {
        LoadDataGame ();
        LoadDataGudang ();
        LoadDataShop ();
        LoadDataKolam ();
    }
    //== IKAN ==
    public static Ikan getIkanbyType (type_ikanBudidya jenisIkan) {
        Ikan temp_data = new Ikan ();

        foreach (Ikan_data item in dataBase.dataIkan) {
            if (item.data.jenisIkan == jenisIkan) {
                temp_data = item.data;
                break;
            }
        }
        return temp_data;
    }
    public static Ikan getIkanbyID (int id) {
        foreach (Ikan_data item in dataBase.dataIkan) {
            if (item.data.id == id) {
                return item.data;
            }
        }
        return null;
    }

    //===----------------------------------------------------------------------------------------------------------------
    //== KOLAM ==
    public static List<Kolam_data> getKolamData_All () {
        return dataKolam;
    }
    public static Kolam_data getDataKolamByID (int id) {
        Kolam_data temp_data = new Kolam_data ();

        foreach (Kolam_data item in dataKolam) {
            if (item.dataKolam.id == id) {
                temp_data = item;
                break;
            }
        }

        return temp_data;
    }
    //++ SAVE LOAD
    public static void SaveDataKolam () {
        BinaryFormatter bf = new BinaryFormatter ();

        FileStream file = File.Create (savePath + "/klm_dat.dta");

        List<Kolam_data> levelData = new List<Kolam_data> ();
        levelData = dataKolam;

        bf.Serialize (file, levelData);
        file.Close ();

        Debug.Log ("Make SaveFile");
    }
    public static void LoadDataKolam () {
        if (File.Exists (savePath + "/klm_dat.dta")) {
            BinaryFormatter bf = new BinaryFormatter ();
            //FileStream file = File.Open(Application.persistentDataPath + "/levelData.sd", FileMode.Open); // 
            FileStream file = File.Open (savePath + "/klm_dat.dta", FileMode.Open);
            List<Kolam_data> data = (List<Kolam_data>) bf.Deserialize (file);
            file.Close ();

            for (int i = 0; i < data.Count; i++) {
                dataKolam[i] = data[i];
            }
        }
    }

    //====---------------------------------------------------------------------------------------------------------------

    //== GUDANG ==
    //PAKAN
    public static float GetCurrentPakan (pakan_type type) {
        return dataBangunan.data_gudang.GetPakanByType (type).jumlahPakan;
    }
    public static Pakan GetPakanDataById (int id) {
        Pakan temp = null;
        foreach (Pakan item in dataBangunan.data_gudang.dataPakan) {
            if (item.idPakan == id) {
                temp = item;
            }
        }
        return temp;
    }
    public static Pakan GetPakanDataByType (pakan_type type) {
        Pakan temp = null;
        foreach (Pakan item in dataBangunan.data_gudang.dataPakan) {
            if (item.type == type) {
                temp = item;
            }
        }
        return temp;
    }

    //OBAT
    public static Obat GetObatbyID (int id) {
        Obat temp_data = new Obat ();

        foreach (Obat item in dataBangunan.data_gudang.dataObat) {
            if (item.idObat == id) {
                temp_data = item;
                break;
            }
        }
        return temp_data;
    }

    //++ SAVE LOAD
    public static void SaveDataGudang () {

        dataBangunan.Save ();
    }
    public static void LoadDataGudang () {
        dataBangunan.Load ();
    }
    //====---------------------------------------------------------------------------------------------------------------

    //== SHOP ==

    public static ItemShop getShopDatabyId (int id) {
        ItemShop temp_data = new ItemShop ();

        foreach (ItemShop item in dataShop) {
            if (item.id == id) {
                temp_data = item;
                break;
            }
        }
        return temp_data;
    }
    public static List<ItemShop> getShopbyType (Item_type type) {
        List<ItemShop> temp_result = new List<ItemShop> ();

        foreach (ItemShop item in dataShop) {
            if (item.type == type) {
                temp_result.Add (item);
            }
        }

        return temp_result;

    }

    //++ SAVE LOAD
    public static void SaveDataShop () {
        BinaryFormatter bf = new BinaryFormatter ();
        //FileStream file = File.Create(Application.persistentDataPath + "/levelData.sd"); // 
        FileStream file = File.Create (savePath + "/shp_dat.dta");

        List<ItemShop> shopData = new List<ItemShop> ();
        shopData = dataShop;

        bf.Serialize (file, shopData);
        file.Close ();
    }
    public static void LoadDataShop () {
        if (File.Exists (savePath + "/shp_dat.dta")) {
            BinaryFormatter bf = new BinaryFormatter ();
            //FileStream file = File.Open(Application.persistentDataPath + "/levelData.sd", FileMode.Open); // 
            FileStream file = File.Open (savePath + "/shp_dat.dta", FileMode.Open);

            List<ItemShop> data = (List<ItemShop>) bf.Deserialize (file);
            file.Close ();

            for (int i = 0; i < data.Count; i++) {
                dataShop[i] = data[i];
            }
        }
    }

    //====---------------------------------------------------------------------------------------------------------------

    //== GAME PLAYS DATA ==

    public static PlayerStatistik GetGamePlayData_PlayerData () {
        return dataGamePlay.playerData;
    }
    public static TimeGame GetGamePlayData_GameTime () {
        return dataGamePlay.timeGame;
    }

    public static PlayerStatistik2 GetGameStatistic () {
        return dataStatistik;
    }

    //++ SAVE LOAD
    public static void SaveDataGame () {
        dataStatistik.Save ();
        dataGamePlay.SavePlayerProgress ();
        dataGamePlay.SavePlayerTime ();
    }
    public static void LoadDataGame () {
        dataStatistik.Load ();
        dataGamePlay.LoadPlayerProgress ();
        dataGamePlay.LoadPLayerTime ();

        TimeController.SetTime (dataGamePlay.timeGame);
    }

    //====---------------------------------------------------------------------------------------------------------------
}

[Serializable]
public class Ikan_data {
    public string nama;
    public Ikan data;
}

[Serializable]
public class otherBuildingData {
    public Gudang data_gudang;

    public void Save () {
        BinaryFormatter bf = new BinaryFormatter ();
        //FileStream file = File.Create(Application.persistentDataPath + "/levelData.sd"); // 
        FileStream file = File.Create (DataBase.savePath + "/gdg_dat.dta");

        Gudang gameProg = new Gudang ();
        gameProg = data_gudang;

        bf.Serialize (file, gameProg);
        //gameData.playerData.Save();
        file.Close ();
    }

    public void Load () {
        if (File.Exists (DataBase.savePath + "/gdg_dat.dta")) {
            BinaryFormatter bf = new BinaryFormatter ();
            //FileStream file = File.Open(Application.persistentDataPath + "/levelData.sd", FileMode.Open); // 
            FileStream file = File.Open (DataBase.savePath + "/gdg_dat.dta", FileMode.Open);

            Gudang data = (Gudang) bf.Deserialize (file);
            file.Close ();

            data_gudang = data;
        }
    }

}

[System.Serializable]
public class GamePlayData {
    public PlayerStatistik playerData;
    public TimeGame timeGame;

    public void SavePlayerProgress () {
        BinaryFormatter bf = new BinaryFormatter ();
        //FileStream file = File.Create(Application.persistentDataPath + "/levelData.sd"); // 
        FileStream file = File.Create (DataBase.savePath + "/gem_datProg.dta");

        PlayerStatistik gameProg = new PlayerStatistik ();
        gameProg = playerData;

        bf.Serialize (file, gameProg);
        //gameData.playerData.Save();
        file.Close ();
    }
    public void SavePlayerTime () {
        BinaryFormatter bf = new BinaryFormatter ();
        //FileStream file = File.Create(Application.persistentDataPath + "/levelData.sd"); // 
        FileStream file = File.Create (DataBase.savePath + "/gem_datTime.dta");

        TimeGame gameTim = new TimeGame ();
        gameTim = timeGame;

        bf.Serialize (file, gameTim);
        //gameData.playerData.Save();
        file.Close ();
    }

    public void LoadPlayerProgress () {
        if (File.Exists (DataBase.savePath + "/gem_datProg.dta")) {
            BinaryFormatter bf = new BinaryFormatter ();
            //FileStream file = File.Open(Application.persistentDataPath + "/levelData.sd", FileMode.Open); // 
            FileStream file = File.Open (DataBase.savePath + "/gem_datProg.dta", FileMode.Open);

            PlayerStatistik data = (PlayerStatistik) bf.Deserialize (file);
            //data.playerData.Load();
            file.Close ();

            playerData = data;
        }
    }
    public void LoadPLayerTime () {
        if (File.Exists (DataBase.savePath + "/gem_datTime.dta")) {
            BinaryFormatter bf = new BinaryFormatter ();
            //FileStream file = File.Open(Application.persistentDataPath + "/levelData.sd", FileMode.Open); // 
            FileStream file = File.Open (DataBase.savePath + "/gem_datTime.dta", FileMode.Open);

            TimeGame data = (TimeGame) bf.Deserialize (file);
            //data.playerData.Load();
            file.Close ();

            timeGame = data;
        }
    }
}