using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour {
    BoxCollider2D fishArea;
    Vector3 destination;
    float maxY, maxX;
    float minX, minY;
    public float speed;
    public GameObject cureEffect;
    public GameObject[] sickEffect;
    float oriSpeed;

    KolamActionManager kolamManager;

    public type_penyakit penyakit;
    // Start is called before the first frame update
    void Start () {
        oriSpeed = speed;
        fishArea = GetComponentInParent<BoxCollider2D> ();

        maxX = fishArea.bounds.max.x;
        minX = fishArea.bounds.min.x;

        maxY = fishArea.bounds.max.y;
        minY = fishArea.bounds.min.y;

        destination = getNewPos ();
        // Debug.Log(fishArea.name);

        kolamManager = FindObjectOfType<KolamActionManager> ();
    }

    // Update is called once per frame
    private void FixedUpdate () {
        Moving ();
        if (!LeanTween.isTweening (cureEffect)) cureEffect.SetActive (false);
    }

    Vector2 getNewPos () {
        Vector2 newDest = new Vector2 (Random.Range (minX, maxX),
            Random.Range (minY, maxY));

        return newDest;
    }

    void Moving () {

        if (kolamManager.feedingFish) {
            speed = oriSpeed * 2;
        } else {
            speed = oriSpeed;
        }
        if (Vector2.Distance (this.transform.position, destination) <= 0.1f) {
            destination = getNewPos ();
        }
        transform.position = Vector2.MoveTowards (this.transform.position, destination, speed * Time.fixedDeltaTime);
        //TopDown
        Vector3 heading = destination - transform.position;
        float sudut = (Mathf.Atan2 (heading.y, heading.x)) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler (0, 0, sudut - 90);
    }

    public void CuredFish () {
        Kolam_data temp_data = kolamManager.GetDataKolam ();
        temp_data.dataKolam.chance_penyakit -= 0.2f;

        if (temp_data.dataKolam.chance_penyakit < 0) temp_data.dataKolam.chance_penyakit = 0;

        temp_data.dataKolam.chance_penyakit -= 0.2f;

        cureEffect.SetActive (true);
        LeanTween.scaleX (cureEffect, 1, 1);
        LeanTween.scaleY (cureEffect, 1, 1);

        SoundControl.playSoundFX (SoundType.cure);

        UpdateStats ();
    }

    public void UpdateStats () {
        sickEffect[0].SetActive (false);
        sickEffect[1].SetActive (false);
        sickEffect[2].SetActive (false);
        penyakit = type_penyakit.empty;

    }
    public void SetFishSick () {
        if (penyakit == type_penyakit.penyakit1) sickEffect[0].SetActive (true);
        else if (penyakit == type_penyakit.penyakit2) sickEffect[1].SetActive (true);
        else if (penyakit == type_penyakit.penyakit3) sickEffect[2].SetActive (true);

    }
}