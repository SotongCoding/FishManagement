using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestViewHandler : MonoBehaviour {
    public bool isMainQuest;

    [Header ("MainQuest")]
    public Text questObjective;
    [Header ("SubQuest")]
    public Text requ_fishAmount;
    public Text requ_fishQuality;
    public Text requ_fishWeight;
    public Image fishImage;
    [Header ("General")]
    public Text tittle;
    public Text rewardGold;
    public Text rewardExp;
    MainQuest mainQuestData;
    SubQuest_progress subQuestData;

    public GameObject detail_obj;
    public Button getRewardButton;

    // Update is called once per frame
    public void Initialize (MainQuest main_data) {
        mainQuestData = main_data;
        tittle.text = mainQuestData.tittle;
        questObjective.text = mainQuestData.dialouge;
        rewardExp.text = mainQuestData.exp.ToString ();
        rewardGold.text = mainQuestData.gold.ToString ();

        getRewardButton.interactable = main_data.CheckRequairment ();

    }
    public void Initialize (SubQuest_progress sub_data) {
        subQuestData = sub_data;
        tittle.text = subQuestData.tittle;
        requ_fishAmount.text = subQuestData.kebutuhanIkan.ToString () + "x";
        requ_fishQuality.text = subQuestData.qualityIkan.ToString ();
        requ_fishWeight.text = subQuestData.beratIkan.ToString ("0.00") + "g";

        rewardExp.text = subQuestData.exp.ToString ();
        rewardGold.text = subQuestData.gold.ToString ();

        fishImage.sprite = SpriteLoader.LoadSpriteFish (subQuestData.jenisIkan);

        // getRewardButton.interactable = sub_data.CheckRequairment ();
    }

    public void ShowDeatail () {
        detail_obj.SetActive (!detail_obj.activeSelf);
    }
    public void GetReward () {
        if (isMainQuest) {
            mainQuestData.GetReward ();
            FindObjectOfType<UI_Quest> ().ReloadMainQuest ();
            Destroy (this.gameObject);
        }
    }
}