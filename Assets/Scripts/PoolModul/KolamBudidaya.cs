using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KolamBudidaya : GetIRefreshing {
    PointerEventData pointerEventData;
    GraphicRaycaster raycaster;

    public int idKolam;
    public int unlockPrice;
    public GameObject unlockSign;
    //PopUpUI;

    [Header ("PopUpP Properti")]
    public GameObject popUp;
    public GameObject[] icons; //0.lapar, 1.racun, 2.panen, 3.oksigen, 4.ph, 5.kebersihan;
    Kolam_data allStatus;
    public bool[] isIconShow = new bool[6];
    int cek_index = 0;

    public float changeRate;
    float cur_changeRate;

    public PlayerControl playerMove;
    private void Start () {
        //unlockSign.SetActive (!DataBase.getDataKolamByID (idKolam).unlock);
        allStatus = DataBase.getDataKolamByID (idKolam);
        //sallStatus.SetKolamBudidaya (this);
        playerMove = FindObjectOfType<PlayerControl> ();
        raycaster = FindObjectOfType<GraphicRaycaster> ();
    }

    private void Update () {
        if (allStatus.unlock) {
            if (cur_changeRate <= 0) {
                ShowIcon ();
                Invoke ("CheckStatus", 0.3f);
                cur_changeRate = changeRate;
            } else {
                cur_changeRate -= Time.deltaTime;
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
            // Check if finger is over a UI element
            if (EventSystem.current.IsPointerOverGameObject (Input.GetTouch (0).fingerId)) {
                Debug.Log ("Touched the UI");
            }
        }
    }

    public void CheckStatus () {
        if (allStatus.dataIkan.jenisIkan != type_ikanBudidya.empty) {
            isIconShow[0] = allStatus.feedAble;
            isIconShow[1] = allStatus.dataKolam.penyakit != type_penyakit.empty? true : false;
            isIconShow[2] = allStatus.dataIkan.berat >= 50 ? true : false;
            isIconShow[3] = allStatus.dataKolam.oksigen <= 1000 ? true : false;
            isIconShow[4] = allStatus.dataKolam.ph > 8 || allStatus.dataKolam.ph < 6.5f ? true : false;
            isIconShow[5] = allStatus.dataKolam.kebersihan < 40 ? true : false;
        } else {
            popUp.SetActive (false);
            foreach (GameObject item in icons) {
                item.SetActive (false);
            }
        }

    }
    public void ShowIcon () {
        if (allStatus.dataKolam.ikan != type_ikanBudidya.empty) {
            for (int i = cek_index; i < isIconShow.Length; i++) {
                if (!icons[i].activeSelf) {
                    if (isIconShow[i]) {
                        foreach (GameObject item in icons) {
                            item.SetActive (false);
                        }

                        popUp.SetActive (true);
                        icons[i].SetActive (true);
                        Debug.LogWarning ("tampilkan : " + icons[0].name);
                        cek_index = i;

                        if (cek_index >= 5) {
                            cek_index = 0;
                        }
                        break;
                    } else {
                        cek_index = 0;
                    }
                }
            }
            if (!isIconShow.Contains (true)) {
                popUp.SetActive (false);
                foreach (GameObject item in icons) {
                    item.SetActive (false);
                }
            }
        }
    }

    //OpenKolamControl
    public void OpenKolam () {
        Kolam_data data = DataBase.getDataKolamByID (idKolam);
        FindObjectOfType<WorldManager> ().OpenKolamControl (data);

        SoundControl.playSoundFX (SoundType.klik);
    }
    public void UnlockKolam () {
        if (!DataBase.getDataKolamByID (idKolam).unlock) {
            //Show Unlock sign when not unlocked
            unlockSign.SetActive (true);

        } else if (DataBase.getDataKolamByID (idKolam).unlock) {
            //Already Unlock
            OpenKolam ();
        }
    }
    public void Unlocking (bool validation) {
        if (validation) {

            if (!DataBase.getDataKolamByID (idKolam).unlock && DataBase.GetGamePlayData_PlayerData ().gold_curent >= unlockPrice) {
                DataBase.getDataKolamByID (idKolam).unlock = true;
                DataBase.GetGamePlayData_PlayerData ().changeGold (-unlockPrice);
                SoundControl.playSoundFX (SoundType.coin);

                DataBase.GetGamePlayData_PlayerData ().kolamUnlock++;
            }
        } else {
            SoundControl.playSoundFX (SoundType.deny);
        }

        unlockSign.SetActive (false);
    }

    // private void OnMouseDown () {

    //     if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
    //         if (!EventSystem.current.IsPointerOverGameObject (Input.touches[0].fingerId)) {
    //             Debug.LogWarning (true);
    //             playerMove.OpenKolam (this);
    //             playerMove.destination.position = this.transform.position;
    //             Invoke ("ToKolam", 1);
    //         }
    //     }
    // }

    public void ToPool () {
        playerMove.destination.position = this.transform.position;
        Invoke ("ToKolam", 0.5f);
    }
    void ToKolam () {
        playerMove.ToKolam ();
        playerMove.OpenKolam (this);
    }

    // public override void UpdatingValue (type_time type) {
    //     if (type == type_time.update_data_kolam) {
    //         Debug.LogWarning ("CheckIcon");
    //         CheckStatus ();
    //     }
    // }
}