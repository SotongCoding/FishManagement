using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

[Serializable]
public class ItemShop
{
    public string name_item;
    public int id;
    public int realID;
    public Item_type type;
    [TextArea]
    public string deskripsi;
    public float price;
    public bool unlock;
    public bool soldOut;
    public string spriteName;

    private const string path = "icons";

    public Sprite LoadSprite()
     {
           Sprite[] sprites = Resources.LoadAll<Sprite> (path);
 
         if(sprites == null)
             return null;
 
         for(int i = 0; i < sprites.Length; i++)
         {
             if(sprites[i].name == spriteName)
                 return (Sprite) sprites[i];
         }
 
         return null;
     }
}

