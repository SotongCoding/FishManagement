using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KolamActionManager : MonoBehaviour {
    [Header ("General Data")]
    Kolam_data data;
    UI_BudidayaKontrol ui_kolam;
    BasketData basket;
    public Transform fishLoc;

    Vector2 minSize, maxSize;
    Vector2 zoneMin, zoneMax;

    [Header ("Cure")]
    public GameObject obatObj;
    public int obatID;
    public List<Transform> sickFish;

    [Header ("Feed")]
    public GameObject feedObj;
    public RectTransform spawnLoc;
    public GameObject peletPerf;
    int pakanH, pakanM, pakanS;
    public bool feedingFish;

    [Header ("PoolAction")]
    public bool increaseOksigen = false;
    public GameObject poolObject;

    [Header ("Sold")]
    public Text goldRecive;
    public Text soldAmount_txt;
    int soldAmount;

    private void Start () {

        basket = FindObjectOfType<PlayerBasket> ().basket;

        minSize = spawnLoc.rect.min;
        maxSize = spawnLoc.rect.max;

        zoneMin = new Vector2 (
            minSize.x + peletPerf.GetComponent<RectTransform> ().rect.width / 2,
            minSize.y + peletPerf.GetComponent<RectTransform> ().rect.height / 2
        );
        zoneMax = new Vector2 (
            maxSize.x - peletPerf.GetComponent<RectTransform> ().rect.width / 2,
            maxSize.y - peletPerf.GetComponent<RectTransform> ().rect.height / 2
        );

        GetFishSick ();

    }
    private void Update () {
        //Update OKsigen
        if (increaseOksigen) {
            if (data.dataKolam.oksigen < 5000 && DataBase.GetGamePlayData_PlayerData ().gold_curent > 10000) {
                data.dataKolam.oksigen += Time.deltaTime * 200;
                DataBase.GetGamePlayData_PlayerData ().changeGold (-(int) (2000 * Time.deltaTime));
            } else if (data.dataKolam.oksigen > 5000) {
                data.dataKolam.oksigen = 5000;
                increaseOksigen = false;
            } else {
                increaseOksigen = false;
            }
        }
    }
    private void OnEnable () {
        data = FindObjectOfType<WorldManager> ().getDataKolam ();
        ui_kolam = FindObjectOfType<UI_BudidayaKontrol> ();

    }

    public Kolam_data GetDataKolam () {
        return data;
    }
    //Ikan

    public void SelectFeed () {
        feedObj.SetActive (!feedObj.activeSelf);
    }
    public void FeedFish (int jenisPakan) {

        if (data.feedAble) {

            float pakanDiGudang = DataBase.GetCurrentPakan ((pakan_type) jenisPakan);
            float feedAmount = (int) (data.dataKolam.beratMasa * 0.3f) / 1.7f;
            //float feedAmount = 500;

            if (feedAmount > pakanDiGudang) {
                DennyMessage.ShowNotif ("Pakan di Gudang tak cukup");
            } else {
                DataBase.GetPakanDataByType ((pakan_type) jenisPakan).jumlahPakan -= feedAmount;

                //lakukan perubahan value kesehatan pada ikan
                data.dataKolam.kebersihan -= data.dataKolam.jumlah_ikan * 0.002f;
                data.dataIkan.lapar_durability += data.dataKolam.beratMasa / 1.7f;
                data.feedAble = false;

                data.dataIkan.CalculatingGrowthRate (CalculatingGrowth (jenisPakan));

                Invoke ("AnimPelet", 0);
                Invoke ("AnimPelet", 0.5f);
                Invoke ("AnimPelet", 1);

                //Updating UI

                ui_kolam.UpdateUI ();
            }
        } else {
            DennyMessage.ShowNotif ("Ikan belum Lapar");
        }
    }

    void AnimPelet () {
        GameObject pelet = Instantiate (peletPerf, spawnLoc);

        pelet.transform.localPosition = new Vector2 (
            Random.Range (zoneMin.x + 5, zoneMax.x - 5),
            Random.Range (zoneMin.y + 6, zoneMax.y - 6)
        );

        LeanTween.scaleX (pelet, 1, 1.3f);
        LeanTween.scaleY (pelet, 1, 1.3f);

        LeanTween.alphaCanvas (pelet.GetComponent<CanvasGroup> (), 0, 4);
        StartCoroutine ("EndFishAnim_Makan", pelet);

    }
    float CalculatingGrowth (int pakan) {
        float diff = (int) data.dataIkan.ukuran - pakan;
        float result = data.dataIkan.ori_growth_rate - (data.dataIkan.ori_growth_rate * diff * 0.5f);
        return result <= 0 ? (data.dataIkan.ori_growth_rate * 0.25f) : result;
    }
    IEnumerator EndFishAnim_Makan (GameObject pelet) {
        feedingFish = true;

        yield return new WaitForSeconds (6);
        Destroy (pelet);
        feedingFish = false;
    }
    public void GetFishSick () {

        if (fishLoc.transform.childCount > 0) {
            foreach (Transform item in fishLoc.transform) {
                if (item.GetComponent<FishAI> ().penyakit != type_penyakit.empty) {
                    sickFish.Add (item);
                }
            }

            obatID = (int) data.dataKolam.penyakit;
        }

    }

    public void SelectCure () {
        obatObj.SetActive (!obatObj.activeSelf);
    }
    public void GiveCure (int code) {
        if (obatID != 0 && obatID == code) {
            if (DataBase.GetObatbyID (code).jumlah_obat > 0) {
                //Cek Data Ikan
                if (sickFish.Count > 0) {
                    sickFish[data.dataKolam.ikan_sakit - 1].GetComponent<FishAI> ().CuredFish ();
                    sickFish.RemoveAt (data.dataKolam.ikan_sakit - 1);

                    DataBase.GetObatbyID (code).jumlah_obat -= 1;
                    data.dataKolam.ikan_sakit -= 1;
                    data.dataIkan.penyakit_durability -= 20;

                    if(data.dataKolam.ikan_sakit <= 0){
                        data.dataKolam.penyakit = type_penyakit.empty;
                    }
                } else {
                    DennyMessage.ShowNotif("Tidak Ada Ikan yang sakit");
                }
            } else {
               DennyMessage.ShowNotif("Obat Kosong");
            }
        } else {
            DennyMessage.ShowNotif("Obat Salah");
        }
    }
    //Kolam

    public void SelectActionPool () {
        poolObject.SetActive (!poolObject.activeSelf);
    }
    public void ControlPH (int code) {
        Obat temp = DataBase.GetObatbyID (code);
        if (temp.jumlah_obat > 0) {
            //Play Anim
            data.dataKolam.ph += temp.idObat == 21 ? -0.3f : 0.3f;

            temp.jumlah_obat -= 1;
            ui_kolam.UpdateUI ();
            SoundControl.playSoundFX (SoundType.klik);

        } else SoundControl.playSoundFX (SoundType.deny);
    }
    public void ControlOksigen () {
        if (increaseOksigen == false) {
            if (data.dataKolam.oksigen < 5000 && data.dataKolam.ikan != type_ikanBudidya.empty) {
                increaseOksigen = true;
                ui_kolam.UpdateUI ();
                SoundControl.playSoundFX (SoundType.klik);
            } else {
                SoundControl.playSoundFX (SoundType.deny);
            }
        } else {
            increaseOksigen = false;
            ui_kolam.UpdateUI ();
        }
    }
    public void ControlCleaner () {
        if (data.dataKolam.kebersihan <= 40) {

            //Play Anim

            data.dataKolam.kebersihan += Random.Range (30, 50);
            data.dataKolam.oksigen += Random.Range (300, 500);

            ui_kolam.UpdateUI ();
            SoundControl.playSoundFX (SoundType.klik);
        }
    }

    //Sold Control
    public void IncreaseSoldAmount (int multiply) {
        soldAmount += multiply;

        if (soldAmount > data.dataKolam.jumlah_ikan) {
            soldAmount = data.dataKolam.jumlah_ikan;
        } else if (soldAmount < 0) {
            soldAmount = 0;
        }
        SoundControl.playSoundFX (SoundType.klik);

        soldAmount_txt.text = soldAmount.ToString ();

        //goldRecive.text = Enums.ConverterNumber (soldAmount * data.dataIkan.jualDasar);
    }
    public void Solding () {
        if (data.dataIkan.berat >= 50 && (data.dataIkan.lapar_durability >= (data.dataKolam.beratMasa * 0.2f))) {
            if (!basket.isContain) {
                if (soldAmount > 0) {
                    //BaseDo
                    basket.InitialStatusFish (data.dataIkan, soldAmount);
                    FindObjectOfType<PlayerBasketNotif> ().OpenNotif ();
                    data.dataKolam.jumlah_ikan -= soldAmount;
                    FindObjectOfType<PlayerBasket> ().SaveProgress ();

                    //Add Panen
                    DataBase.GetGameStatistic ().GetPanenDataByFishType (data.dataKolam.ikan).
                    AddPanen ((int) data.dataIkan.berat, (int) data.dataIkan.kualitas, soldAmount);

                    if (data.dataKolam.jumlah_ikan == 0) {
                        data.dataIkan = new Ikan_ChangeAble ();
                    }
                    soldAmount = 0;
                    soldAmount_txt.text = soldAmount.ToString ();

                    if (data.dataKolam.jumlah_ikan == 0) {
                        foreach (Transform item in fishLoc.transform) {
                            Destroy (item.gameObject);
                        }
                        ui_kolam.UpdateUI ();
                        ui_kolam.OpenResetKolam ();
                        data.dataKolam.ikan = type_ikanBudidya.empty;
                    }

                }

                SoundControl.playSoundFX (SoundType.klik);
            } else {
                SoundControl.playSoundFX (SoundType.deny);
                DennyMessage.ShowNotif ("Basket sudah ada isi nya. Kosongnkan dulu");
                Debug.LogWarning ("Basket sudah ada isi nya. Kosongnkan dulu");
            }
        } else {
            SoundControl.playSoundFX (SoundType.deny);
            if (data.dataIkan.berat < 50) {
                DennyMessage.ShowNotif ("Ukuran kurang besar");
                Debug.Log ("Ukuran kurang besar");
            } else {
                DennyMessage.ShowNotif ("Beri Pakan ikan sampai tingkat kenyang >20%");
                Debug.Log ("Beri Pakan Ikan sampai tingkat kenyang >20%");
            }
        }
    }

    public void ShowHint (GameObject showObject) {
        showObject.SetActive (!showObject.activeSelf);
        StartCoroutine ("CloseObject", showObject);

        Debug.LogWarning ("show Hint");
    }

    IEnumerator CloseObject (GameObject selectedObject) {
        yield return new WaitForSeconds (3f);

        selectedObject.SetActive (false);
    }
}