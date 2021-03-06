using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName = "SubQuestAsset", fileName = "subQuest")]
public class SubQuest : ScriptableObject {
    public int questLevel;
    public int subQuestID;

    [TextArea]
    public string dialouge;

    [Header ("Reward")]
    public int goldMin;
    public int goldMax;
    public int expMin;
    public int expMax;

    [Header ("Requirement of Quest")]

    public List<SubQuestSoldFish> require = new List<SubQuestSoldFish> ();
}

[System.Serializable]
public class SubQuestSoldFish {
    public type_ikanBudidya jenisIkan;
    [Header ("Kebutuhan Ikan")]
    public int butuhMin;
    public int butuhMax;
    [Header ("Quality")]
    public int qualityMin;
    public int qualityMax;
    [Header("Berat")]
    public float wightMin;
    public float weightMax;
}
