using System;
using System.Collections.Generic;

[Serializable]
public class Gudang
{
    public int level;
    public int subLevel;
    public List<Pakan> dataPakan = new List<Pakan>();
    public List<Obat> dataObat = new List<Obat>();

    public Pakan GetPakanbyId(int id)
    {

        Pakan temp = null;
        foreach (Pakan item in dataPakan)
        {
            if (item.idPakan == id)
            {
                temp = item;
            }
        }
        return temp;
    }
    public Pakan GetPakanByType(pakan_type type)
    {
        Pakan temp = null;
        foreach (Pakan item in dataPakan)
        {
            if (item.type == type)
            {
                temp = item;
            }
        }
        return temp;
    }
    public Obat GetObatbyId(int id)
    {

        Obat temp = null;
        foreach (Obat item in dataObat)
        {
            if (item.idObat == id)
            {
                temp = item;
            }
        }
        return temp;
    }
    public Obat GetPakanByType(obat_type type)
    {
        Obat temp = null;
        foreach (Obat item in dataObat)
        {
            if (item.type == type)
            {
                temp = item;
            }
        }
        return temp;
    }
}
[Serializable]
public class Pakan
{
    public string namaPakan;
    public int idPakan;
    public pakan_type type;
    public float jumlahPakan;
}

[Serializable]
public class Obat
{
    public string namaObat;
    public int idObat;
    public obat_type type;
    public int jumlah_obat;

}


