using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scalecharacter : MonoBehaviour
{

    // Use this for initialization

    public static Scalecharacter scalecharacter;
    public GameObject moc;
    Vector3 oldScale = new Vector3(1, 1, 1);
    Vector3 newScale = new Vector3(1.3f, 1.3f, 1.3f);
    public float smoothing;
    public int ID;

    //public Color lerpColor = Color.white;
    Color LightColor = new Color(255, 255, 255, 255);
    Color darkColor = new Color(0, 0, 0, 255);
    public Image dameScreen;
    public float smoothingColor;
    void Start()
    {

        scalecharacter = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(moc.transform.position.x - transform.position.x) < 30)
        {
            if (ID == 1)
            {
                SwitchCharacter.Sc.btnChoose[0].SetActive(false);
                SwitchCharacter.Sc.price[0].SetActive(false);
                SwitchCharacter.Sc.price[1].SetActive(false);
                SwitchCharacter.Sc.price[2].SetActive(false);
                SwitchCharacter.Sc.btnChoose[1].SetActive(false);
                SwitchCharacter.Sc.btnChoose[2].SetActive(false);
            }
            else
                if (ID == 2)
                {
                    if (PlayerPrefs.GetInt("buy_char_ca") == 0)
                    {
                        SwitchCharacter.Sc.price[0].SetActive(true);
                        SwitchCharacter.Sc.btnChoose[0].SetActive(true);
                    }
                    else
                    {
                        SwitchCharacter.Sc.price[0].SetActive(false);
                        SwitchCharacter.Sc.btnChoose[0].SetActive(false);

                    }
                    SwitchCharacter.Sc.price[1].SetActive(false);
                    SwitchCharacter.Sc.price[2].SetActive(false);
                    SwitchCharacter.Sc.btnChoose[1].SetActive(false);
                    SwitchCharacter.Sc.btnChoose[2].SetActive(false);
                }
                else
                    if (ID == 3)
                    {
                        if (PlayerPrefs.GetInt("buy_char_caduoi") == 0)
                        {
                            SwitchCharacter.Sc.price[1].SetActive(true);
                            SwitchCharacter.Sc.btnChoose[1].SetActive(true);

                        }
                        else
                        {
                            SwitchCharacter.Sc.price[1].SetActive(false);
                            SwitchCharacter.Sc.btnChoose[1].SetActive(false);
                        }
                        SwitchCharacter.Sc.price[0].SetActive(false);
                        SwitchCharacter.Sc.price[2].SetActive(false);
                        SwitchCharacter.Sc.btnChoose[0].SetActive(false);
                        SwitchCharacter.Sc.btnChoose[2].SetActive(false);
                    }
                    else
                        if (ID == 4)
                        {
                            if (PlayerPrefs.GetInt("buy_char_bachtuoc") == 0)
                            {
                                SwitchCharacter.Sc.price[2].SetActive(true);
                                SwitchCharacter.Sc.btnChoose[2].SetActive(true);
                            }
                            else
                            {
                                SwitchCharacter.Sc.price[2].SetActive(false);
                                SwitchCharacter.Sc.btnChoose[2].SetActive(false);
                            }
                            SwitchCharacter.Sc.price[0].SetActive(false);
                            SwitchCharacter.Sc.price[1].SetActive(false);
                            SwitchCharacter.Sc.btnChoose[0].SetActive(false);
                            SwitchCharacter.Sc.btnChoose[1].SetActive(false);
                        }


            transform.localScale = Vector3.Lerp(newScale, oldScale, smoothing * Time.deltaTime);
            dameScreen.color = Color.Lerp(darkColor, LightColor, smoothingColor * Time.deltaTime);
        }
        else
        {

            dameScreen.color = Color.gray;
            transform.localScale = Vector3.Lerp(oldScale, newScale, smoothing * Time.deltaTime);
        }
    }
}
