using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ShopUIControl : MonoBehaviour {
    public GameObject fish_ActionBox, food_ActionBox, other_ActionBox;
    public GameObject main_ActionBox;
    public TextMeshProUGUI deskripsi_text;

    string[] kalimat_pembuka = {
        "Ah, Selamat datang silakan melihat lihat",
        "Selamat datang. \nKami punya beberapa kebutuhan yang kau perlukan untuk melakukan budidaya",
        "Selamat Datang. \nIkan dan pakan kami selalu pada kondisi yang terbaik",
        "Obat yang kami sediakan sudah dipermudah agar tinggal tabur dan penyakit langsung hilang"
    };

    //Buy Fish
    [Header ("Fish Buy Action")]
    public Button[] poolbtn;
    public Text buyAmount_text_fish;
    public Text totalPrize_text_fish;
    public Text itemName_fish;
    public Image itemImage_fish;
    [Header ("Food Buy Action")]
    public Text buyAmount_text_food;
    public Text totalPrize_text_food;
    public Text itemName_food;
    public Image itemImage_food;

    [Header ("Medic Buy Action")]
    public Text buyAmount_text_medic;
    public Text totalPrize_text_medic;
    public Text itemName_medic;
    public Image itemImage_medic;

    ShopControl shopControl;
    bool increase = false, decrease = false;
    float holdTime = 1;
    float cur_HoldTime = 0;

    private void Start () {
        TutorControl.ShowTutor (6);
        shopControl = FindObjectOfType<ShopControl> ();
        int ran = Random.Range (0, kalimat_pembuka.Length);
        deskripsi_text.text = kalimat_pembuka[ran];
    }
    private void Update () {
        if (increase && cur_HoldTime > holdTime) {
            if (shopControl.cur_itemType == Item_type.pakan)
                shopControl.buyAmount += 100;
            else
                shopControl.buyAmount += 1;

            SoundControl.playSoundFX (SoundType.klik);

        } else if (decrease && cur_HoldTime > holdTime) {
            if (shopControl.cur_itemType == Item_type.pakan)
                shopControl.buyAmount -= 100;
            else
                shopControl.buyAmount -= 1;

            SoundControl.playSoundFX (SoundType.coin);
        } else if (increase || decrease) {
            cur_HoldTime += Time.deltaTime;
        }

        if (shopControl.buyAmount < 0) {
            shopControl.buyAmount = 0;
        }

        if (shopControl.cur_itemType == Item_type.benih) {
            buyAmount_text_fish.text = shopControl.buyAmount.ToString ();
            totalPrize_text_fish.text = Enums.ConverterNumber ((shopControl.buyAmount * shopControl.choosenItemData.price));
            itemName_fish.text = shopControl.choosenItemData.name_item;

        } else if (shopControl.cur_itemType == Item_type.pakan) {
            buyAmount_text_food.text = shopControl.buyAmount.ToString ();
            totalPrize_text_food.text = Enums.ConverterNumber ((shopControl.buyAmount * shopControl.choosenItemData.price));
            itemName_food.text = shopControl.choosenItemData.name_item;

        } else if (shopControl.cur_itemType == Item_type.medic) {
            buyAmount_text_medic.text = shopControl.buyAmount.ToString ();
            totalPrize_text_medic.text = Enums.ConverterNumber ((shopControl.buyAmount * shopControl.choosenItemData.price));
            itemName_medic.text = shopControl.choosenItemData.name_item;
        }
    }

    public void OpenCloseShop () {

        if (SceneManager.GetSceneByName ("Shop").isLoaded) {
            SceneManager.UnloadSceneAsync ("Shop");
            shopControl.buyAmount = 0;
            shopControl.poolID = 0;
        } else {
            SceneManager.LoadScene ("Shop", LoadSceneMode.Additive);
        }

        SoundControl.playSoundFX (SoundType.klik);
    }

    public void CloseActionUI () {
        fish_ActionBox.SetActive (false);
        food_ActionBox.SetActive (false);
        other_ActionBox.SetActive (false);

        main_ActionBox.SetActive (false);

        shopControl.buyAmount = 0;
        ResetUI ();

        SoundControl.playSoundFX (SoundType.klik);

        int ran = Random.Range (0, kalimat_pembuka.Length);
        deskripsi_text.text = kalimat_pembuka[ran];
    }

    public void OpenUI (Item_type typeItem) {
        main_ActionBox.SetActive (true);

        if (typeItem == Item_type.benih) {
            for (int i = 0; i < 3; i++) {
                poolbtn[i].interactable = DataBase.getKolamData_All () [i].canUse;
                poolbtn[i].gameObject.SetActive (DataBase.getKolamData_All () [i].unlock);
            }

            fish_ActionBox.SetActive (true);
            itemImage_fish.sprite = shopControl.choosenItemData.LoadSprite ();

        } else if (typeItem == Item_type.pakan) {
            food_ActionBox.SetActive (true);
            itemImage_food.sprite = shopControl.choosenItemData.LoadSprite ();
        } else {
            other_ActionBox.SetActive (true);
            itemImage_medic.sprite = shopControl.choosenItemData.LoadSprite ();
        }

        deskripsi_text.text = shopControl.choosenItemData.deskripsi;

        SoundControl.playSoundFX (SoundType.klik);
    }

    public void choosePool (int poolID) {
        if (shopControl.poolID == poolID) {

            poolbtn[poolID - 1].GetComponent<Image> ().color = Color.white;
            shopControl.poolID = 0;
        } else if (shopControl.poolID <= 0) {
            shopControl.poolID = poolID;
            poolbtn[poolID - 1].GetComponent<Image> ().color = Color.green;
        } else {
            poolbtn[shopControl.poolID - 1].GetComponent<Image> ().color = Color.white;
            shopControl.poolID = poolID;
            poolbtn[poolID - 1].GetComponent<Image> ().color = Color.green;
        }

        SoundControl.playSoundFX (SoundType.klik);
    }

    //Btn - + pressed

    public void hold (int value) {
        if (value == -1) {
            decrease = true;
        } else if (value == 1) {
            increase = true;
        }

    }

    public void release () {
        increase = decrease = false;
        cur_HoldTime = 0;
    }
    public void click (int value) {
        if (shopControl.buyAmount > -1) {
            shopControl.buyAmount += 1 * value;
            SoundControl.playSoundFX (SoundType.klik);
        }

        if (shopControl.cur_itemType == Item_type.benih) {
            buyAmount_text_fish.text = shopControl.buyAmount.ToString ();
            totalPrize_text_fish.text = (shopControl.buyAmount * shopControl.choosenItemData.price).ToString ();
        } else if (shopControl.cur_itemType == Item_type.pakan) {
            buyAmount_text_food.text = shopControl.buyAmount.ToString ();
            totalPrize_text_food.text = (shopControl.buyAmount * shopControl.choosenItemData.price).ToString ();
        }

    }

    public void ResetUI () {
        foreach (Button item in poolbtn) {
            item.GetComponent<Image> ().color = Color.white;
        }
    }
}