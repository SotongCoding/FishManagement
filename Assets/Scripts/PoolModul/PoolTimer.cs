using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTimer : MonoBehaviour {

    public int idPool;
    public Kolam_data data;
    float curTime;
    public int counter = 1;
    bool ev1 = true, ev2 = true, ev3 = true;

    void Start () {
        DontDestroyOnLoad (this);
        data = DataBase.getDataKolamByID (idPool);
    }
 
    private void LateUpdate () {
        if (data != null && TimeController.timeOn) {
            if (data.unlock && data.dataIkan.jenisIkan != type_ikanBudidya.empty) {
                curTime += (Time.deltaTime);
                if (curTime > 6f) {
                    curTime = 0;
                    counter++;
                    ev1 = true;
                    ev2 = true;
                    ev3 = true;

                    
                } 
            }

            if (counter % 2 == 0 && ev1) {
                data.UpdatingValueFish ();

                ev1 = false;
            }
            if (counter % 4 == 0 && ev2) {
                data.UpdatingValuePool ();

                ev2 = false;
            }
            if (counter % 12 == 0 && ev3) {
                data.UpdatingValueLast ();

                counter = 1;
                ev3 = false;
            }
        }
    }
}