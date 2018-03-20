using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    // Use this for initialization
    
    public static UIManager uimanager;
    public GameObject gameover;
    public static bool checkMute;
    Animator myanim;
    public GameObject Mute, UnMute;
    void Awake()
    {
        if (checkMute)
        {
            Invoke("mute", 0.01f);
            Mute.SetActive(false);
            UnMute.SetActive(true);
        }
        else
        {
            Invoke("unmute", 0.01f);
            Mute.SetActive(true);
            UnMute.SetActive(false);
        }
       
        myanim = GetComponent<Animator>();
        uimanager = this;
        Time.timeScale = 1f;
        //GameObject.Find("Pause").GetComponent<Button>().onClick.AddListener(() => showexitgame())
         
    }
    void mute()
    {
      
        SoundManager.soundManager.music.mute = true;
      
    }
    void unmute()
    {
        
        SoundManager.soundManager.music.mute = false;
    }
    
    void Start()
    {
     
    }
  
    public bool checkgameover;
    public void GameOver()
    {
        checkgameover = true;
        Destroy(GameObject.Find("Box"));
    //    SpwanBox.allowButon = false;
        Time.timeScale = 0f;
        gameover.SetActive(true);
        SpwanBox.allowButon = false;
        ShareRateAds.shareRateAds.ShowIn();

    }
    public void Restart()
    {
        if (!checkMute)
            SoundManager.soundManager.music.mute = false;
        else
            SoundManager.soundManager.music.mute = true;
        gameover.SetActive(false);
        PlayerPrefs.SetInt("watchfullvideo", 0);
        SceneManager.LoadScene(2);
        SpwanBox.allowButon = true;
        
       
        
    }
    public void Rate()
    {
        ShareRateAds.shareRateAds.Rate();
    }
    public void Share()
    {
        ShareRateAds.shareRateAds.Share();
    }
    public void Video()
    {
        ShareRateAds.shareRateAds.ShowBasedVideo();
    }
    public void BuyCharacter()
    {
        SceneManager.LoadScene(1);
        DontDestroyOnLoad(SoundManager.soundManager.music);
        SoundManager.soundManager.music.mute = false;
    }
    public void mutemusic()
    {
        checkMute = true;
        Mute.SetActive(false);
        UnMute.SetActive(true);
    }
    public void Unmutemusic()
    {
        checkMute = false;
        Mute.SetActive(true);
        UnMute.SetActive(false);
    }
    
   
    
     
}
