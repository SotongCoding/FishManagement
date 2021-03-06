using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControl : MonoBehaviour {
    // Start is called before the first frame update

    public GameObject shopItem_perf;
    public Transform itempos;

    //FishBuy
    public int poolID;

    //General
    public int buyAmount;

    public ItemShop choosenItemData;
    public Item_type cur_itemType;

    List<ItemShop> openedItem = new List<ItemShop> ();
    ShopUIControl uiShop;
    void Start () {
        Open_Food ();
        uiShop = FindObjectOfType<ShopUIControl> ();
    }

    public void Open_Fish () {
        ClearShop ();
        cur_itemType = Item_type.benih;
        openedItem = DataBase.getShopbyType (Item_type.benih);

        CreateItem ();
        SoundControl.playSoundFX (SoundType.klik);
    }
    public void Open_Food () {
        ClearShop ();
        cur_itemType = Item_type.pakan;
        openedItem = DataBase.getShopbyType (Item_type.pakan);

        CreateItem ();
        SoundControl.playSoundFX (SoundType.klik);
    }

    public void Open_Medic () {
        ClearShop ();
        cur_itemType = Item_type.medic;
        openedItem = DataBase.getShopbyType (Item_type.medic);

        CreateItem ();
        SoundControl.playSoundFX (SoundType.klik);
    }
    public void Open_Other () {
        ClearShop ();
        cur_itemType = Item_type.other;
        openedItem = DataBase.getShopbyType (Item_type.other);

        CreateItem ();
        SoundControl.playSoundFX (SoundType.klik);
    }

    void CreateItem () {
        foreach (ItemShop item in openedItem) {
            GameObject cl_item = Instantiate (shopItem_perf);

            cl_item.GetComponentInChildren<ItemViewHandler> ().Initial (item);
            cl_item.GetComponentInChildren<Button> ().onClick.AddListener (delegate { OpenActionBox (item.type, item); });

            cl_item.transform.SetParent (itempos);
            cl_item.transform.localPosition = Vector3.zero;
            cl_item.transform.localScale = Vector3.one;

        }
    }

    void ClearShop () {
        cur_itemType = Item_type.none;
        openedItem.Clear ();

        foreach (Transform obj in itempos) {
            Destroy (obj.gameObject);
        }
    }

    void OpenActionBox (Item_type type, ItemShop itemData) {
        choosenItemData = itemData;
        uiShop.OpenUI (type);
    }

    public void Buy () {
        //Pembelian Benih
        if (cur_itemType == Item_type.benih) {
            if (poolID == 0) {
                DennyMessage.ShowNotif ("Klik nomor kolam yang akan di taruh bibit");
                uiShop.deskripsi_text.text = "Ingat kolam tidak akan bisa kau isi ikan jika sudah ada Ikannya.\nPanen ikan sebelumnya untuk memasukan ikan yang baru ke dalam Kolam";
            }
            if (CheckBenih ()) {
                Ikan fish = DataBase.getIkanbyID (choosenItemData.realID);
                Kolam_data pool = DataBase.getDataKolamByID (poolID);

                pool.dataIkan = new Ikan_ChangeAble (fish);
                pool.dataIkan.ukuran = type_ikansize.kecil;

                pool.dataKolam.jumlah_ikan = buyAmount;
                pool.dataKolam.ikan = fish.jenisIkan;
                pool.dataKolam.beratMasa = pool.dataKolam.jumlah_ikan * pool.dataIkan.berat;
                pool.dataIkan.lapar_durability = pool.dataKolam.beratMasa;

                pool.canUse = false;
                pool.feedAble = true;

                DataBase.GetGamePlayData_PlayerData ().changeGold (-(buyAmount * (int) choosenItemData.price));

                //Reset
                FindObjectOfType<WorldManager> ().OpenKolamControl (DataBase.getDataKolamByID (poolID));
                poolID = 0;
                uiShop.CloseActionUI ();

            }
        } else if (cur_itemType == Item_type.pakan) {
            if (CheckPakan ()) {
                DataBase.GetPakanDataById (choosenItemData.realID).jumlahPakan += buyAmount;
                DataBase.GetGamePlayData_PlayerData ().changeGold (-(buyAmount * (int) choosenItemData.price));

                uiShop.CloseActionUI ();
            }
        } else if (cur_itemType == Item_type.medic) {
            if (CheckMedic ()) {
                DataBase.GetObatbyID (choosenItemData.realID).jumlah_obat += buyAmount;
                DataBase.GetGamePlayData_PlayerData ().changeGold (-(buyAmount * (int) choosenItemData.price));

                uiShop.CloseActionUI ();
            }
        }

    }

    bool CheckPakan () {
        //Pakan temp_data = DataBase.GetPakanDataById (choosenItemData.realID);

        if (DataBase.GetGamePlayData_PlayerData ().gold_curent >= choosenItemData.price * buyAmount) {
            SoundControl.playSoundFX (SoundType.coin);
            return true;
        } else {
            if (DataBase.GetGamePlayData_PlayerData ().gold_curent < choosenItemData.price * buyAmount) {
                DennyMessage.ShowNotif ("Uang anda Kurang");
            }
            SoundControl.playSoundFX (SoundType.deny);
            return false;
        }
    }
    bool CheckBenih () {
        Kolam_data temp_pool = DataBase.getDataKolamByID (poolID);

        if (
            temp_pool.canUse && buyAmount > 0 &&
            DataBase.GetGamePlayData_PlayerData ().gold_curent >= choosenItemData.price * buyAmount

        ) {
            SoundControl.playSoundFX (SoundType.coin);
            return true;
        } else {
            if (buyAmount <= 0) {
                DennyMessage.ShowNotif ("Tentukan berapa banyak bibit yang di beli");
            } else if (DataBase.GetGamePlayData_PlayerData ().gold_curent < choosenItemData.price * buyAmount) {
                DennyMessage.ShowNotif ("Uang anda Kurang");
            }
            SoundControl.playSoundFX (SoundType.deny);
            return false;
        }
    }
    bool CheckMedic () {
        if (DataBase.GetGamePlayData_PlayerData ().gold_curent >= choosenItemData.price * buyAmount) // Uang
        {
            SoundControl.playSoundFX (SoundType.coin);
            return true;
        } else {
            if (DataBase.GetGamePlayData_PlayerData ().gold_curent < choosenItemData.price * buyAmount) {
                DennyMessage.ShowNotif ("Uang anda Kurang");
            }
            SoundControl.playSoundFX (SoundType.deny);
            return false;
        }
    }
}