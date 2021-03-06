using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour {

    static SoundControl sounds;
    public List<AudioClip> clips = new List<AudioClip> ();
    private void Start () {
        if (sounds == null) {
            DontDestroyOnLoad (this.gameObject);
            sounds = this;
        } else if (sounds != this) {
            Destroy (this.gameObject);
        }
    }
    public AudioSource bg, effect_sound;

    
    public Scrollbar bg_s,fx_s;
    public Toggle bg_t, fx_t;

   
    public static void playSoundFX (SoundType sound) {
        sounds.effect_sound.PlayOneShot (sounds.clips[(int) sound]);
    }

    public void OnBGChange () {
        bg.volume = bg_s.value;
    }
    public void OnSFXChange () {
        effect_sound.volume = fx_s.value;
    }

    public void EnableBG () {
        bg.enabled = bg_t.isOn;
    }
    public void EnableSFX () {
        effect_sound.enabled = fx_t.isOn;
    }
}

public enum SoundType {
    klik = 0,
    coin = 1,
    deny = 2,
    task_clear = 3,
    water = 4,
    cure = 5
}