using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatistik {
    //Player Gold
    public int gold_curent;
    public int gold_use;
    public int gold_get;
    public int kolamUnlock;

    public int level = 0;
    public int curEXP = 0;
    public int[] reqEXP = { 500, 1500, 3000, 5000, 7500 };

    //Progess Tutor;

    public int first = 0, openKolam = 0, openShop = 0, jualIkan = 0, openTugas = 0, openNPC = 0, isPlayed = 0;

    //Panen-Jual

    // Uang -EXP
    public void changeGold (int value) {
        gold_curent += value;

        if (value > 0) gold_get += value;
        else if (value < 0) gold_use -= value;
    }
    public void increaseEXP (int value) {
        curEXP += value;
        if (curEXP >= reqEXP[level]) {
            level++;
        }

    }
}

[System.Serializable]
public class Statik_PanenData {
    public type_ikanBudidya ikan;
    public int panen_kualitas_50;
    public int panen_kualitas_70;
    public int panen_kualitas_90;

    public int panen_berat_50;
    public int panen_berat_100;
    public int panen_berat_200;

    public int total;

    public int SUM () {
        return total;
    }

    public int GetPanenQuality_andMore (int value) {
        if (value >= 90) {
            return panen_kualitas_90;
        } else if (value >= 70) {
            return panen_kualitas_70;
        } else if (value >= 50) {
            return panen_kualitas_50;
        } else return 0;
    }
    void AddPanenQuality_andMore (int value, int amount) {
        if (value >= 90) {
            panen_kualitas_90 += amount;
        }
        if (value >= 70) {
            panen_kualitas_70 += amount;
        }
        if (value >= 50) {
            panen_kualitas_50 += amount;
        }
    }

    public int GetPanenBerat_andMore (int value) {
        if (value >= 200) {
            return panen_berat_200;
        } else if (value >= 100) {
            return panen_berat_100;
        } else if (value >= 50) {
            return panen_berat_50;
        } else return 0;
    }
    void AddPanenBerat_andMore (int value, int amount) {
        if (value >= 200) {
            panen_berat_200 += amount;
        }
        if (value >= 100) {
            panen_berat_100 += amount;
        }
        if (value >= 50) {
            panen_berat_50 += amount;
        }
    }

    public void AddPanen (int berat, int kualitas, int amount) {
        AddPanenBerat_andMore ((int) berat, amount);
        AddPanenQuality_andMore ((int) kualitas, amount);

        total += amount;
    }

}

[System.Serializable]
public class Statik_SellData {
    public type_ikanBudidya ikan;
    public int jual_kualitas_50;
    public int jual_kualitas_70;
    public int jual_kualitas_90;

    public int jual_berat_50;
    public int jual_berat_100;
    public int jual_berat_200;

    public int total;

    public int SUM () {
        return total;
    }

    public int GetJualQuality_andMore (int value) {
        if (value >= 90) {
            return jual_kualitas_90;
        } else if (value >= 70) {
            return jual_kualitas_70;
        } else if (value >= 50) {
            return jual_kualitas_50;
        } else return 0;
    }
    void AddJualQuality_andMore (int value, int amount) {
        if (value >= 90) {
            jual_kualitas_90 += amount;
        }
        if (value >= 70) {
            jual_kualitas_70 += amount;
        }
        if (value >= 50) {
            jual_kualitas_50 += amount;
        }
    }

    public int GetJualBerat_andMore (int value) {
        if (value >= 200) {
            return jual_berat_200;
        } else if (value >= 100) {
            return jual_berat_100;
        } else if (value >= 50) {
            return jual_kualitas_50;
        } else return 0;
    }
    void AddJualBerat_andMore (int value, int amount) {
        if (value >= 200) {
            jual_berat_200 += amount;
        }
        if (value >= 100) {
            jual_berat_100 += amount;
        }
        if (value >= 50) {
            jual_berat_50 += amount;
        }
    }

    public void AddJual (int berat, int kualitas, int amount) {
        AddJualBerat_andMore ((int) berat, amount);
        AddJualQuality_andMore ((int) kualitas, amount);

        total += amount;
    }
}