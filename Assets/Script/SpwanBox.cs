using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpwanBox : MonoBehaviour
{

    SpwanBox sp;
    Vector3 vt;
    public GameObject box;
    public static bool allowButon;

    public bool spawn;
    void Start()
    {
        sp = new SpwanBox();
        allowButon = true;
        spawn = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (allowButon)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (spawn) { spawn = false; spwanbox(); }


            }
            if (Input.GetMouseButtonUp(0))
            {
                if (spawn)
                    spawn = false;
                else
                    spawn = true;
            }

        }

    }

    void spwanbox()
    {
        vt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vt = new Vector3(vt.x, vt.y, 1);
        Instantiate(box, vt, Quaternion.identity);

    }




}
