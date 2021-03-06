using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour {
    public AIPath _path;
    public Transform destination;
    Animator anim;
    KolamBudidaya temp_data_kolam;
    NPCInteract selectedNPC;

    bool toKolam, toNPC;

    [HideInInspector] public Vector2 lastDir;
    Vector2 curDir;

    void Start () {
        _path = this.GetComponent<AIPath> ();
        anim = GetComponent<Animator> ();
    }
    public bool IsStoped () {
        return _path.reachedEndOfPath;
    }
    public void ToKolam () {
        toKolam = true;
        toNPC = false;

    }
    public void ToOther () {
        toKolam = false;
        toNPC = false;

    }
    public void ToNPC () {
        toKolam = false;
        toNPC = true;

    }

    public void ToNull () {
        toKolam = false;
        toNPC = false;

    }

    public void OpenKolam (KolamBudidaya data) {
        temp_data_kolam = data;
    }
    public void OpenPreviewQuest (NPCInteract data) {
        selectedNPC = data;
    }

    void Update () {
        // if(Input.GetMouseButtonDown(0)){
        //     destination.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // }

        if (_path.reachedDestination) {
            if (toKolam) {
                temp_data_kolam.UnlockKolam ();
            }
            if (toNPC) {
                selectedNPC.previewQuest ();
            }
            ToNull ();
            
        }

        if (_path.reachedDestination) {
            lastDir = (destination.position - this.transform.position).normalized;
            anim.SetBool ("run", false);
            anim.SetFloat ("x", lastDir.x);
            anim.SetFloat ("y", lastDir.y);

        } else {
            curDir = _path.velocity.normalized;
            anim.SetBool ("run", true);
            anim.SetFloat ("x", curDir.x);
            anim.SetFloat ("y", curDir.y);
        }
    }

}