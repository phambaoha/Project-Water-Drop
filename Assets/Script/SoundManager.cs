using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    // Use this for initialization
    // tao doi tuong tinh khong thay doi qua cac scene
    public static SoundManager soundManager;
    public AudioSource music;

    public AudioSource sound;

    public AudioClip[] audioclip;

    void Awake()
    {
        music.mute = false;
        DontDestroyOnLoad(gameObject);// không xoá đối tuong khi load scene
        // tu khoi tao lai doi tuong, minh không hiểu là gì
        soundManager = this;
    }

   
    public void playSound(int index)
    {
        sound.clip = audioclip[index];
        sound.Play();
    }
}
