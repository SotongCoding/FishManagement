using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCInteract : MonoBehaviour {

    public bool isClearingNPC;
    public SubQuest_progress questData;
    public bool isTakeQuest;
    public GameObject sellUI;
    public GameObject TriviaUI;
    public TextMeshProUGUI triviaText;

    PlayerControl playerMove;
    BasketData basket;

    private void Start () {
        playerMove = FindObjectOfType<PlayerControl> ();
        sellUI = GameObject.Find ("SellingHUD").transform.GetChild (0).gameObject;

        if (isClearingNPC) Initialize (false, 0, null);
        else LeanTween.moveLocalX (this.gameObject, 1.65f, 2);
    }

    private void Update () {

    }
    public void Initialize (bool isLoad, int questID, NPCData visualData) {
        if (!isClearingNPC) {
            isTakeQuest = isLoad;
            questData = isLoad ? DataBase_quest.GetSubQuestByID (questID) : DataBase_quest.GenerateSubQuest ();
            questData.npc = visualData;
        } else {
            isTakeQuest = true;
            basket = playerMove.GetComponent<PlayerBasket> ().basket;
        }

    }

    public void MovingNPC () {
        playerMove.destination.position = this.transform.position;
        Invoke ("ToNPC", 0.5f);
    }
    void CreateSellUIDataForClearingNPC () {
        questData.jenisIkan = 0 + basket.jenisIkan;
        questData.kebutuhanIkan = 0 + basket.jumlahIkan;
        questData.beratIkan = (int) 0 + basket.beratIkan;
        questData.qualityIkan = 0 + (int) basket.kualitasIkan;

    }

    void ToNPC () {
        playerMove.ToNPC ();
        playerMove.OpenPreviewQuest (this);
    }

    public void previewQuest () {
        if (isClearingNPC) {
            CreateSellUIDataForClearingNPC ();
            if (questData.jenisIkan != type_ikanBudidya.empty) {
                sellUI.GetComponent<SellUIControl> ().InitialInfo (questData, this);
                sellUI.SetActive (true);
                CloseTrivia ();
            } else {
                showTrivias ();
            }

        } else {
            sellUI.GetComponent<SellUIControl> ().InitialInfo (questData, this);
            sellUI.SetActive (true);
        }

    }
    public void TakeQuest () {
        DataBase_quest.AddSubQuest (questData);
        isTakeQuest = true;
    }

    public void OutFrame () {
        this.transform.localScale = new Vector3 (-1, 1, 1);
        LeanTween.moveX (this.gameObject, transform.position.x - 14, 2.5f);
        Destroy (this.gameObject, 3f);
    }

    private void OnDestroy () {
        DataBase_quest.SaveQuestProgress ();
        FindObjectOfType<NPCHandler> ().npc_amount--;
    }

    public void showTrivias () {
        TriviaUI.SetActive (true);
        triviaText.text = Enums.GetTrivia ();
        Invoke ("CloseTrivia", 60);
    }
    public void CloseTrivia () {
        triviaText.text = " ";
        triviaText.rectTransform.anchoredPosition = new Vector2 (0.05f, 0);
        TriviaUI.SetActive (false);
    }
}