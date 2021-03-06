using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewHandler : MonoBehaviour {

    public Text price;
    public Image item_pic;
    public void Initial (ItemShop data) {
        this.GetComponent<Button> ().interactable = data.unlock;

        price.text = Enums.ConverterNumber (data.price);
        item_pic.sprite = data.LoadSprite ();
    }
}