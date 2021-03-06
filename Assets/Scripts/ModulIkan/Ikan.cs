using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu]
public class Ikan : ScriptableObject {
    public int id;
    public type_ikanBudidya jenisIkan;
    public type_air jenisAir;
    public type_ikansize ukuran;
    public float berat;
    public float umur;
    public float health;
    public float kualitas;

    public float growth_rate;
    public float penggunaan_pakan;

    public float penyakit_durability;
    public float lapar_durability;

    public int jualDasar;
    }