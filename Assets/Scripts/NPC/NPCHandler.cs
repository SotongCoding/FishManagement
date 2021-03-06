using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCHandler : MonoBehaviour {
    static NPCHandler npc_handler;

    public int npc_amount;
    // public List<NPCData> spawnedNPC = new List<NPCData> ();
    public GameObject npcPerfabs;
    Transform parentPlace;
    bool isOnce;
    float curTime = 0;
    int spawnRate = 10;

    // Start is called before the first frame update
    private void Awake () {
        if (npc_handler == null) {
            DontDestroyOnLoad (this.gameObject);
            npc_handler = this;
        } else if (npc_handler != this) {
            Destroy (this.gameObject);
        }

        curTime = 10;
    }

    private void Start () {
        DataBase_quest.LoadSubQuest ();
        parentPlace = GameObject.Find ("NPCPlaces").transform;

        if (DataBase_quest.GetCurentSubQuest ().Count > 0) {
            foreach (SubQuest_progress item in DataBase_quest.GetCurentSubQuest ()) {
                if (item.id != 0) GenerateNPC (item);
            }
        }
    }

    private void Update () {
        if (curTime <= 0) {
            if (npc_amount < 3) {
                GenerateNPC ();
            }

            curTime = spawnRate;
        } else {
            curTime -= Time.deltaTime;
        }
    }

    // Genarate new NPC
    void GenerateNPC () {
        int color_code, hatCode, bodyCode;
        int posCode = 0;

        foreach (Transform item in parentPlace.transform) {
            if (item.childCount > 0) {
                posCode += 1;
            } else {
                GameObject npcObj = Instantiate (npc_handler.npcPerfabs, npc_handler.parentPlace);
                //Visual
                color_code = Random.Range (0, 3);
                hatCode = Random.Range (0, 2);
                bodyCode = Random.Range (0, 3);
                npcObj.GetComponent<NPCView> ().Initialize (color_code, hatCode, bodyCode);

                //Data
                npcObj.GetComponent<NPCInteract> ().Initialize (false, 0, new NPCData {
                    posCode = posCode,
                        colorCode = color_code, hatCode = hatCode, bodyCode = bodyCode
                });

                npcObj.transform.SetParent (parentPlace.transform.GetChild (posCode));

                npcObj.transform.localPosition = new Vector2 (0, -0.15f);

                npc_amount++;
                break;
            }
        }

        //Save Data
    }
    void GenerateNPC (SubQuest_progress data) {
        int posCode = data.npc.posCode;

        GameObject npcObj = Instantiate (npcPerfabs, parentPlace);

        //Visual
        npcObj.GetComponent<NPCView> ().Initialize (data.npc.colorCode,
            data.npc.hatCode, data.npc.bodyCode);

        //Data
        npcObj.GetComponent<NPCInteract> ().Initialize (true, data.id, data.npc);

        npcObj.transform.SetParent (parentPlace.transform.GetChild (posCode));

        npcObj.transform.localPosition = new Vector2 (0, -0.15f);

        npc_amount++;
    }
    public void RemoveNPC () {
        npc_amount--;
    }
}

[System.Serializable]
public class NPCData {
    public int posCode;
    public int hatCode, bodyCode;
    public int colorCode;
}