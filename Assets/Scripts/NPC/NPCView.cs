using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCView : MonoBehaviour {
    public SpriteRenderer hat, body;
    public List<Sprite> body_list, hat_list;

    public Color[] baseColor;
    public void Initialize (int color, int hat, int body) {
        this.hat.sprite = hat_list[hat];
        this.body.sprite = body_list[body];

        this.hat.color = this.body.color = baseColor[color];
    }
}