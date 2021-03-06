using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenControl : MonoBehaviour
{
    // Start is called before the first frame update
   private void Awake() {
    Screen.SetResolution(1280,720,true,60);    
   }
}
