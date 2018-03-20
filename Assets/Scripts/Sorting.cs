using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorting : MonoBehaviour
{
    public GameObject[] tail;

    private TrailRenderer trail;
    // tao 1 material để dạng sprite/default
    public Material material;
    public Color[] color;
    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        trail.sortingLayerName = "bong";
        trail.sortingOrder = 10;
        StartCoroutine(randomcolor());
     
        
    }
    void Update()
    {

        if (!UIManager.uimanager.checkgameover)
        {    
            if(tail[0].active)
            transform.position = tail[0].transform.position;
            else
            if (tail[1].active)
                transform.position = tail[1].transform.position;
            else if (tail[2].active)
                transform.position = tail[2].transform.position;
             else if (tail[3].active)
                transform.position = tail[3].transform.position;
        }
    }

   IEnumerator  randomcolor()
   {
      yield return new WaitForSeconds(3);
       int ran = Random.Range(0,6);
       Changecolor(ran);

       StartCoroutine(randomcolor());
       
   }
    public void Changecolor(int i)
    {
        material.color = color[i];
    }
}
