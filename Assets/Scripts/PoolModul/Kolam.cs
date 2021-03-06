using System;
using UnityEngine;

[Serializable]
public class Kolam {
    public int id;
    public type_kolam tipe;
    public type_air air;
    public type_ikanBudidya ikan;
    public type_penyakit penyakit = type_penyakit.empty;

    public int ikan_sakit;
    public int jumlah_ikan;
    public float beratMasa;

    public float ph;
    public float oksigen;
    public float chance_penyakit;
    public float kebersihan;
    public void GetPenyakit () {
        float number = UnityEngine.Random.Range (0, 101);
        if (number < chance_penyakit) {
            int random1 = UnityEngine.Random.Range (11, 14); // jenis penyakit
            penyakit = (type_penyakit) random1;

            int random2; //jumlah sakit

            if (jumlah_ikan <= 10) {
                random2 = 1;
            } else {
                random2 = UnityEngine.Random.Range (1, 5);
            }

            ikan_sakit = random2;
            DataBase.getDataKolamByID (id).dataIkan.penyakit_durability = ikan_sakit * 20;

            if(UnityEngine.MonoBehaviour.FindObjectOfType<WorldManager>().getDataKolam().dataKolam.id == id){
                UnityEngine.MonoBehaviour.FindObjectOfType<SpwanIkan>().CreateSickFishOnOpenKolam();
                
            }
        }
    }
    public void getFishDeath (Ikan_ChangeAble dataIkan) {
        int number = 100;
        int deathNumber = 0;

        if (dataIkan.health < 35) {
            number = UnityEngine.Random.Range (0, 80);
            deathNumber = UnityEngine.Random.Range (1, 6);

        } else if (dataIkan.health < 10) {
            number = UnityEngine.Random.Range (0, 101);
            deathNumber = UnityEngine.Random.Range (3, 10);

        } else if (dataIkan.health <= 0) {
            number = 0;
            deathNumber = UnityEngine.Random.Range (10, 20);
            jumlah_ikan -= deathNumber;
        }

        if (number < 30) {
            jumlah_ikan -= deathNumber;
            DennyMessage.ShowNotif("Ikan mati sebanyak :\n"+deathNumber);
        }

        jumlah_ikan = jumlah_ikan <= 0 ? 0 : jumlah_ikan;
    }
}