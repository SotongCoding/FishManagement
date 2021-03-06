using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellUIControl : MonoBehaviour {
    public Text tittle;
    public Text kualitas;
    public Text berat;
    public Text jumlahIkan;
    public Image icon;

    public Text gold;
    public Text exp;

    [Header ("Button")]

    public Button sellButton;
    public Button takeButton;
    public Button discardButton;

    public NPCInteract selectedNPC;
    public PlayerBasket player;

    private void Start () {
        //player = FindObjectOfType<PlayerBasket> ();

    }

    public void CloseUI () {
        this.gameObject.SetActive (false);
    }

    public void InitialInfo (SubQuest_progress data, NPCInteract NPC) {
        tittle.text = data.tittle;
        jumlahIkan.text = data.kebutuhanIkan.ToString () + "x";
        kualitas.text = data.qualityIkan.ToString ();
        berat.text = data.beratIkan.ToString ("0.00") + "g";

        exp.text = data.exp.ToString ();
        gold.text = data.gold.ToString ();

        icon.sprite = SpriteLoader.LoadSpriteFish (data.jenisIkan);

        selectedNPC = NPC;

        if (NPC.isTakeQuest) {
            takeButton.gameObject.SetActive (false);
            discardButton.gameObject.SetActive (false);
            CompareQuest ();
        } else {
            takeButton.gameObject.SetActive (true);
            discardButton.gameObject.SetActive (true);
            sellButton.interactable = false;
        }

        if (player == null) {
            player = FindObjectOfType<PlayerBasket> ();
        }
    }

    public void TakeQuest () {
        selectedNPC.TakeQuest ();
        takeButton.gameObject.SetActive (false);
        discardButton.gameObject.SetActive (false);
        CompareQuest ();

        DataBase_quest.SaveQuestProgress ();
    }

    public void SellFish () {
        player.basket.SoldFish (selectedNPC.isClearingNPC);

        DataBase.GetGameStatistic ().GetSellDataByFishType (selectedNPC.questData.jenisIkan).
        AddJual ((int) selectedNPC.questData.beratIkan, selectedNPC.questData.qualityIkan, selectedNPC.questData.kebutuhanIkan);

        DataBase.GetGamePlayData_PlayerData ().increaseEXP (selectedNPC.questData.exp);

        if (!selectedNPC.isClearingNPC) {
            int id = selectedNPC.questData.id;
            DataBase_quest.ClearSubQuestbyID (id);
            FindObjectOfType<NPCHandler> ().RemoveNPC ();

            selectedNPC.OutFrame ();
        } else {
            this.gameObject.SetActive (false);
            selectedNPC.showTrivias ();
        }

        player.SaveProgress ();

        CloseUI ();
        FindObjectOfType<PlayerBasketNotif> ().CheckFish ();
    }
    public void DiscardQuest () {
        this.gameObject.SetActive (false);
        selectedNPC.OutFrame ();
        FindObjectOfType<NPCHandler> ().RemoveNPC ();
    }

    void CompareQuest () {

        player.basket.InitialStatusSell (selectedNPC.questData, false);
        sellButton.gameObject.SetActive (true);
        sellButton.interactable = player.basket.CheckSellFish ();

    }
}