using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLoader : MonoBehaviour {
    // Start is called before the first frame update
    private const string path = "icons";

    public static Sprite LoadSpriteFish (type_ikanBudidya jenis) {
        Sprite[] sprites = Resources.LoadAll<Sprite> (path);
        string spriteName = "";

        if (jenis == type_ikanBudidya.leleLokal) spriteName = "lele_lokal";
        if (jenis == type_ikanBudidya.leleJumbo) spriteName = "lele_jumbo";
        if (jenis == type_ikanBudidya.leleSangkuriang) spriteName = "lele_sangkuriang";

        if (sprites == null)
            return null;

        for (int i = 0; i < sprites.Length; i++) {
            if (sprites[i].name == spriteName)
                return (Sprite) sprites[i];
        }

        return null;
    }public static Sprite LoadSpriteFish (string spriteName) {
        Sprite[] sprites = Resources.LoadAll<Sprite> (path);
        
        if (sprites == null)
            return null;

        for (int i = 0; i < sprites.Length; i++) {
            if (sprites[i].name == spriteName)
                return (Sprite) sprites[i];
        }

        return null;
    }
}