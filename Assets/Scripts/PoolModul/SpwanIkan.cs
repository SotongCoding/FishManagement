using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpwanIkan : MonoBehaviour {
    [Header ("Position")]
    public Transform parent_pos;
    public GameObject[] fish_perfs;

    Canvas thisSceneCanvas;
    KolamActionManager kolamManager;
    private void Awake () {
        kolamManager = FindObjectOfType<KolamActionManager> ();
    }

    // Start is called before the first frame update
    void Start () {
        thisSceneCanvas = GetComponent<Canvas> ();
        thisSceneCanvas.worldCamera = FindObjectOfType<Camera> ();

        // Bounds area = collide.bounds;       
    }

    public void CreateFish (int fish_amount, type_ikanBudidya typeIkan) {
        int createAmount = 0;
        int sickfishAmount = 0;

        if (fish_amount > 0) {
            SoundControl.playSoundFX (SoundType.water);
        }

        if (fish_amount < 20) createAmount = fish_amount;
        else if (fish_amount < 20 && fish_amount >= 100) createAmount = fish_amount / 2;
        else createAmount = 50 + (fish_amount / 5);

        createAmount = createAmount > 70 ? 70 : createAmount;

        GameObject temp_fish = fish_perfs[0];
        if (typeIkan == type_ikanBudidya.leleLokal) {
            temp_fish = fish_perfs[0];
        } else if (typeIkan == type_ikanBudidya.leleSangkuriang) {
            temp_fish = fish_perfs[1];
        }

        for (int i = 0; i < createAmount; i++) {
            GameObject fish = Instantiate (temp_fish);
            fish.transform.SetParent (parent_pos);

            fish.transform.localScale = Vector3.one * .3f;
            fish.transform.localPosition = Vector3.zero;

            if (sickfishAmount < kolamManager.GetDataKolam ().dataKolam.ikan_sakit) {
                fish.GetComponent<FishAI> ().penyakit = kolamManager.GetDataKolam ().dataKolam.penyakit;
                fish.GetComponent<FishAI> ().SetFishSick ();
                sickfishAmount++;
            }

        }

    }
    public void CreateSickFishOnOpenKolam () {
        for (int i = 0; i < kolamManager.GetDataKolam ().dataKolam.ikan_sakit; i++) {
            parent_pos.transform.GetChild (i).GetComponent<FishAI> ().penyakit = kolamManager.GetDataKolam ().dataKolam.penyakit;
            parent_pos.transform.GetChild (i).GetComponent<FishAI> ().GetComponent<FishAI> ().SetFishSick ();
        }

        kolamManager.GetFishSick();
    }
}