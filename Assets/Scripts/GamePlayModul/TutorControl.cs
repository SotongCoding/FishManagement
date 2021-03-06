using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorControl : MonoBehaviour {
    public GameObject baseObject;
    public Text dialogText;
    public RectTransform npc_picture;
    public Button dialogBox;
    public GameObject touchIcon;
    //0-Klik kolam 1-Persiapan 2-Icon KOlam1 3-IconKolam2 4-Aksi Kolam 5-Panen Ikan
    public GameObject[] helpObjects, helpObjects2;
    int curEventCode;
    PlayerStatistik gameData;
    GameObject help2;

    static TutorControl tutor;

    private void Awake () {
        if (tutor == null) {
            DontDestroyOnLoad (this.gameObject);
            tutor = this;
        } else if (tutor != this) {
            Destroy (this.gameObject);
        }
        help2 = helpObjects2[0].transform.parent.gameObject;
        //ReScaleSize ();

        //dialogBox.GetComponent<Image> ().color = new Color32 (0, 0, 0, 1);

    }

    private void Start () {
        gameData = DataBase.GetGamePlayData_PlayerData ();

        LeanTween.moveLocalY (touchIcon, 10, 1).setEaseLinear ().setLoopPingPong ();
    }

    void ReScaleSize () {
        SpriteRenderer sr = help2.GetComponent<SpriteRenderer> ();
        help2.transform.localScale = new Vector3 (1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        help2.transform.localScale = new Vector3 ((worldScreenWidth / width),
            (worldScreenHeight / height), 1);
    }

    public static void ShowTutor (int kodeTutor) {
        tutor.dialogBox.onClick.RemoveAllListeners ();

        //Pertema Kali Main Game
        if (kodeTutor == 0 && tutor.gameData.first == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);

            tutor.dialogBox.onClick.AddListener (delegate () { ShowTutor (1); });
        } else if (kodeTutor == 1 && tutor.gameData.first == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);
            tutor.OpenHelpImage2 (0);

            tutor.dialogBox.onClick.AddListener (delegate () { CloseTutor (); });
            tutor.gameData.first = 1;
        }

        //Saat di NPC
        if (kodeTutor == 2 && tutor.gameData.openNPC == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);
            tutor.dialogBox.onClick.AddListener (delegate () { ShowTutor (3); });
            tutor.OpenHelpImage2 (1);

        } else if (kodeTutor == 3 && tutor.gameData.openNPC == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);
            tutor.dialogBox.onClick.AddListener (delegate () { ShowTutor (4); });
            tutor.OpenHelpImage2 (2);

        } else if (kodeTutor == 4 && tutor.gameData.openNPC == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);
            tutor.dialogBox.onClick.AddListener (delegate () { CloseTutor (); });

            tutor.gameData.openNPC = 1;
        }

        //Saat di Kolam
        if (kodeTutor == 5 && tutor.gameData.openKolam == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);
            tutor.dialogBox.onClick.AddListener (delegate () { CloseTutor (); });
            //tutor.OpenHelpImage (1);
            tutor.OpenHelpImage2 (4);

        } else if (kodeTutor == 8 && tutor.gameData.openKolam == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);
            tutor.dialogBox.onClick.AddListener (delegate () { ShowTutor (10); });
            tutor.OpenHelpImage (2);

        } else if (kodeTutor == 10 && tutor.gameData.openKolam == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);
            tutor.dialogBox.onClick.AddListener (delegate () { ShowTutor (15); });
            tutor.OpenHelpImage (3);

        } else if (kodeTutor == 15 && tutor.gameData.openKolam == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);
            tutor.dialogBox.onClick.AddListener (delegate () { ShowTutor (11); });
            tutor.OpenHelpImage (6);

        } else if (kodeTutor == 11 && tutor.gameData.openKolam == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);
            tutor.dialogBox.onClick.AddListener (delegate () { ShowTutor (13); });
            //tutor.OpenHelpImage (4);
            tutor.OpenHelpImage2 (5);

        } else if (kodeTutor == 13 && tutor.gameData.openKolam == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);
            tutor.dialogBox.onClick.AddListener (delegate () { ShowTutor (9); });
            tutor.OpenHelpImage (4);
            //tutor.OpenHelpImage2 (6);

        } else if (kodeTutor == 9 && tutor.gameData.openKolam == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);
            tutor.dialogBox.onClick.AddListener (delegate () { ShowTutor (14); });
            tutor.OpenHelpImage (5);

        } else if (kodeTutor == 14 && tutor.gameData.openKolam == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);
            tutor.dialogBox.onClick.AddListener (delegate () { ShowTutor (12); });

        } else if (kodeTutor == 12 && tutor.gameData.openKolam == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);
            tutor.OpenHelpImage2 (3);

            tutor.dialogBox.onClick.AddListener (delegate () { CloseTutor (); });
            tutor.gameData.openKolam = 1;
        }

        //Saat di Toko
        if (kodeTutor == 6 && tutor.gameData.openShop == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);

            tutor.dialogBox.onClick.AddListener (delegate () { ShowTutor (16); });
            
        } else if (kodeTutor == 16 && tutor.gameData.openShop == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);

            tutor.OpenHelpImage (7);
            tutor.dialogBox.onClick.AddListener (delegate () { ShowTutor (17); });
        } else if (kodeTutor == 17 && tutor.gameData.openShop == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);
            tutor.OpenHelpImage (8);

            tutor.gameData.openShop = 1;
            tutor.dialogBox.onClick.AddListener (delegate () { CloseTutor (); });
        }

        //Saat Buka Tugas
        if (kodeTutor == 7 && tutor.gameData.openTugas == 0) {
            tutor.baseObject.SetActive (true);
            tutor.help2.SetActive (true);
            tutor.dialogText.text = Enums.ShowTutor (kodeTutor, tutor.npc_picture);
            tutor.OpenHelpImage2 (6);

            tutor.gameData.openTugas = 1;
            tutor.dialogBox.onClick.AddListener (delegate () { CloseTutor (); });
        }
    }

    static void CloseTutor () {
        tutor.baseObject.SetActive (false);
        tutor.CloseAllImage ();
        tutor.CloseAllImage2 ();
        tutor.help2.SetActive (false);
    }

    void OpenHelpImage (int code) {
        CloseAllImage ();
        CloseAllImage2 ();
        helpObjects[code].SetActive (true);
    }
    void OpenHelpImage2 (int code) {
        CloseAllImage ();
        CloseAllImage2 ();
        helpObjects2[code].SetActive (true);
       // dialogBox.GetComponent<Image> ().color = new Color32 (0, 0, 0, 1);
    }
    void CloseAllImage () {
        foreach (GameObject item in helpObjects) {
            item.SetActive (false);
        }
    }
    void CloseAllImage2 () {
        foreach (GameObject item in helpObjects2) {
            item.SetActive (false);
        }
        //dialogBox.GetComponent<Image> ().color = new Color32 (0, 0, 0, 100);
    }
}