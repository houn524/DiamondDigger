using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSoundManager : MonoBehaviour {

#if UNITY_ANDROID && !UNITY_EDITOR
    int digDiamondFileID;
    int digDiamondFeverFileID;
    int digRockFileID;
    int digDiamondSoundID;
    int digDiamondFeverSoundID;
    int digRockSoundID;
#else
    public AudioClip digDiamondSound;
    public AudioClip digDiamondFeverSound;
    public AudioClip digRockSound;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
    void Start() {
        AndroidNativeAudio.makePool();
        digDiamondFileID = AndroidNativeAudio.load("Pickaxe_Ring_A4.wav");
        digDiamondFeverFileID = AndroidNativeAudio.load("FeverHitSound.mp3");
        digRockFileID = AndroidNativeAudio.load("Pickaxe_Rock_B6.wav");
    }
#endif

    public void PlayDigDiamond() {
        
#if UNITY_ANDROID && !UNITY_EDITOR
        digDiamondSoundID = AndroidNativeAudio.play(digDiamondFileID);
#else
        GetComponent<AudioSource>().clip = digDiamondSound;
        GetComponent<AudioSource>().Play();
#endif
    }

    public void PlayDigRock() {
#if UNITY_ANDROID && !UNITY_EDITOR
        digRockSoundID = AndroidNativeAudio.play(digRockFileID);
#else
        GetComponent<AudioSource>().clip = digRockSound;
        GetComponent<AudioSource>().Play();
#endif
    }

    public void PlayDigDiamondFever() {

#if UNITY_ANDROID && !UNITY_EDITOR
        digDiamondFeverSoundID = AndroidNativeAudio.play(digDiamondFeverFileID);
#else
        GetComponent<AudioSource>().clip = digDiamondFeverSound;
        GetComponent<AudioSource>().Play();
#endif
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    void OnApplicationQuit() {
        // Clean up when done
        AndroidNativeAudio.unload(digDiamondFileID);
        AndroidNativeAudio.unload(digDiamondFeverFileID);
        AndroidNativeAudio.unload(digRockFileID);
        AndroidNativeAudio.releasePool();
    }
#endif
}
