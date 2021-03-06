using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class PlayerStatistik2 {
    [SerializeField] private List<Statik_PanenData> dataPanens = new List<Statik_PanenData> ();
    [SerializeField] private List<Statik_SellData> dataSells = new List<Statik_SellData> ();

    public Statik_SellData GetSellDataByFishType (type_ikanBudidya type) {

        Statik_SellData temp_data = new Statik_SellData ();

        foreach (Statik_SellData item in dataSells) {
            if (item.ikan == type) {
                temp_data = item;
                break;
            }
        }
        return temp_data;
    }
    public Statik_PanenData GetPanenDataByFishType (type_ikanBudidya type) {

        Statik_PanenData temp_data = new Statik_PanenData ();

        foreach (Statik_PanenData item in dataPanens) {
            if (item.ikan == type) {
                temp_data = item;
                break;
            }
        }
        return temp_data;
    }

    public void Save () {
        BinaryFormatter bf_s = new BinaryFormatter ();
        BinaryFormatter bf_p = new BinaryFormatter();
        //FileStream file = File.Create(Application.persistentDataPath + "/levelData.sd"); // 
        FileStream file_sell = File.Create (DataBase.savePath + "/gem_dat_static_sell.dta");
        FileStream file_panen = File.Create (DataBase.savePath + "/gem_dat_static_panen.dta");

        List<Statik_SellData> sellData = new List<Statik_SellData> ();
        List<Statik_PanenData> panenData = new List<Statik_PanenData> ();

        sellData = dataSells;
        panenData = dataPanens;

        bf_s.Serialize (file_sell, sellData);
        bf_p.Serialize (file_panen, panenData);

        file_sell.Close ();
        file_panen.Close ();
    }
    public void Load () {
        if (File.Exists (DataBase.savePath + "/gem_dat_static_sell.dta")) {
            BinaryFormatter bf_s = new BinaryFormatter ();
            //FileStream file = File.Open(Application.persistentDataPath + "/levelData.sd", FileMode.Open); // 
            FileStream file = File.Open (DataBase.savePath + "/gem_dat_static_sell.dta", FileMode.Open);

            List<Statik_SellData> data = (List<Statik_SellData>) bf_s.Deserialize (file);
            file.Close ();

            for (int i = 0; i < data.Count; i++) {
                dataSells[i] = data[i];
            }
        }

        if (File.Exists (DataBase.savePath + "/gem_dat_static_panen.dta")) {
            BinaryFormatter bf_p = new BinaryFormatter ();
            //FileStream file = File.Open(Application.persistentDataPath + "/levelData.sd", FileMode.Open); // 
            FileStream file = File.Open (DataBase.savePath + "/gem_dat_static_panen.dta", FileMode.Open);

            List<Statik_PanenData> data = (List<Statik_PanenData>) bf_p.Deserialize (file);
            file.Close ();

            for (int i = 0; i < data.Count; i++) {
                dataPanens[i] = data[i];
            }
        }
    }

}