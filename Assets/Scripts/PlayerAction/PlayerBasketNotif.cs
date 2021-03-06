using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBasketNotif : MonoBehaviour {
    public GameObject notifObj;
    public Image fishIcon;
    public TextMeshProUGUI jumlah;
    public TextMeshProUGUI berat, kualitas;

    BasketData data;
    private void Start () {
        data = FindObjectOfType<PlayerBasket> ().basket;
    }
    private void Update () {
        if (data.isContain) OpenNotif ();
    }
    public void OpenNotif () {
        notifObj.SetActive (true);
        fishIcon.sprite = SpriteLoader.LoadSpriteFish (data.jenisIkan);
        berat.text = data.beratIkan.ToString ("0.00") + "g";
        jumlah.text = data.jumlahIkan.ToString ();
        kualitas.text = ((int) data.kualitasIkan).ToString ("");

    }

    public void CheckFish () {
        if (data.jumlahIkan <= 0) {
            notifObj.gameObject.SetActive (false);
        }
    }
}