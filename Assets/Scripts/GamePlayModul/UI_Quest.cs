using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Quest : MonoBehaviour {
    public Transform questPlace;
    public GameObject basePlace;
    public GameObject mainQuestObject;
    public GameObject subQuestObject;

    bool isOpen = false;

    public void GenerateQuest () {
        if (!isOpen) {
            TutorControl.ShowTutor (7);
            basePlace.SetActive (true);
            MainQuest main_data = DataBase_quest.GetCurentMainQuest ();
            List<SubQuest_progress> progressQuest = DataBase_quest.GetCurentSubQuest ();

            GameObject mQ_obj = Instantiate (mainQuestObject, questPlace);
            mQ_obj.GetComponent<QuestViewHandler> ().Initialize (main_data);

            foreach (SubQuest_progress item in progressQuest) {
                GameObject sQ_obj = Instantiate (subQuestObject, questPlace);
                sQ_obj.GetComponent<QuestViewHandler> ().Initialize (item);

            }
        } else {
            basePlace.SetActive (false);
            foreach (Transform item in questPlace) {
                Destroy (item.gameObject);
            }
        }
        SoundControl.playSoundFX (SoundType.klik);
        isOpen = !isOpen;
    }

    public void ReloadMainQuest () {
        GameObject mQ_obj = Instantiate (mainQuestObject, questPlace);
        mQ_obj.GetComponent<QuestViewHandler> ().Initialize (DataBase_quest.GetCurentMainQuest ());
    }
}