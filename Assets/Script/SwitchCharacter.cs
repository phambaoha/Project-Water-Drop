using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchCharacter : MonoBehaviour
{
    public static SwitchCharacter Sc;

    

    public List<Button> btnactive = new List<Button>();



    public GameObject[] price;
    public GameObject[] btnChoose;


    //  public int index = 0;
    void Awake()
    {
        Sc = this;
        Time.timeScale = 1f;
      
        btnactive[0].interactable = false;
        btnactive[1].interactable = false;
        btnactive[2].interactable = false;

      
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("buy_char_ca") == 1)
        {
            btnactive[0].interactable = true;
        }
        else
            if (PlayerPrefs.GetInt("buy_char_ca") == 0)
            {
                btnactive[0].interactable = false;
            }

        if (PlayerPrefs.GetInt("buy_char_caduoi") == 1)
        {
            btnactive[1].interactable = true;
        }
        else
            if (PlayerPrefs.GetInt("buy_char_caduoi") == 0)
            {
                btnactive[1].interactable = false;
            }
        if (PlayerPrefs.GetInt("buy_char_bachtuoc") == 1)
        {
            btnactive[2].interactable = true;
        }
        else
            if (PlayerPrefs.GetInt("buy_char_bachtuoc") == 0)
            {
                btnactive[2].interactable = false;
            }
    }
    void Update()
    {
        timeselectcharacter();
        
    }
    IEnumerator timeselectcharacter()
    {
        yield return new WaitForSeconds(3);
    }
   
    public void buycharacter_ca()
    {
        Purchaser.purchase.BuyCharacter_ca();
    }
    public void buycharacter_caduoi()
    {
        Purchaser.purchase.BuyCharacter_caduoi();
    }
    public void buycharacter_bachtuoc()
    {
        Purchaser.purchase.BuyCharacter_bachtuoc();
    }
    public void removeAds()
    {
        Purchaser.purchase.RemoveNoAds();
    }
    public void Result_removeAds()
    {
        ShareRateAds.shareRateAds.banner = false;
        ShareRateAds.shareRateAds.inters = false;
        ShareRateAds.shareRateAds.reward = false;
    }
    public void Result_Buycharacter_Bachtuoc()
    {
        btnactive[2].interactable = true;
        btnChoose[2].SetActive(false);
        price[2].SetActive(false);
    }
    public void Result_Buycharacter_Caduoi()
    {
        btnactive[1].interactable = true;
        btnChoose[1].SetActive(false);
        price[1].SetActive(false);
    }

    public void Result_Buycharacter_Ca()
    {
        btnactive[0].interactable = true;
        btnChoose[0].SetActive(false);
        price[0].SetActive(false);

    }

    public void click_bachtuoc()
    {
        PlayerPrefs.SetInt("setactivecharacter", 3);
        SceneManager.LoadScene(2);
    }
    public void click_ca()
    {
        PlayerPrefs.SetInt("setactivecharacter", 1);
        SceneManager.LoadScene(2);

    }
    public void click_caduoi()
    {
        PlayerPrefs.SetInt("setactivecharacter", 2);
        SceneManager.LoadScene(2);
    }
    public void click_nhong()
    {
        PlayerPrefs.SetInt("setactivecharacter", 0);
        SceneManager.LoadScene(2);
    }
}
