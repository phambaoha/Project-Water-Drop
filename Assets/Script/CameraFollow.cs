using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    // Use this for initialization
    public GameObject[] player;
    public float smoothing;
    float distance;
    void Awake()
    {
        //ShareRateAds.shareRateAds.ShowBaner();
        if (PlayerPrefs.GetInt("setactivecharacter") == 0)
        {
            player[0].SetActive(true);
        }
        else
            if (PlayerPrefs.GetInt("setactivecharacter") == 1)
            {
                player[1].SetActive(true);
            }
            else
                if (PlayerPrefs.GetInt("setactivecharacter") == 2)
                {
                    player[2].SetActive(true);
                }
                else
                    if (PlayerPrefs.GetInt("setactivecharacter") == 3)
                    {
                        player[3].SetActive(true);
                    }
        
    }
    void Start()
    {
        if (player[0].active)
            distance = transform.position.y - player[0].transform.position.y;
        else
        if (player[1].active)
            distance = transform.position.y - player[1].transform.position.y;
        else
        if (player[2].active)
            distance = transform.position.y - player[2].transform.position.y;
        else
        if (player[3].active)
            distance = transform.position.y - player[3].transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
      try
      {
          if (player[0].active)
              transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, player[0].transform.position.y + distance, Time.deltaTime * smoothing), -10);
          else if (player[1].active)
              transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, player[1].transform.position.y + distance, Time.deltaTime * smoothing), -10);
          else if (player[2].active)
              transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, player[2].transform.position.y + distance, Time.deltaTime * smoothing), -10);
          else if (player[3].active)
              transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, player[3].transform.position.y + distance, Time.deltaTime * smoothing), -10);
      }
      catch (System.Exception ex)
      {
      	
      }
         
        
        

    }
}
