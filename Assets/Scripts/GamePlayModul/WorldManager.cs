using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour {
    Kolam_data dataOpenKolam;
    otherBuildingData buildingData;

    private void Start () {

    }
    private void Update () {
    
    }

    public void OpenKolamControl (Kolam_data OpenData) {
        if (OpenData.unlock) {
            TutorControl.ShowTutor (5);
            dataOpenKolam = OpenData;

            if (SceneManager.GetSceneByName ("Kolam").isLoaded) {
                SceneManager.UnloadSceneAsync ("Kolam");
            }

            SceneManager.LoadScene ("Kolam", LoadSceneMode.Additive);
        }
    }
    public Kolam_data getDataKolam () {
        return dataOpenKolam;
    }
}