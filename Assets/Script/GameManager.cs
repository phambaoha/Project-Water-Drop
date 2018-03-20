using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] objects;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Zen", 0.5f, 2f);
    }
    void Zen()
    {
       
        GameObject obj = objects[Random.Range(0, objects.Length)];
        if(obj != null)
        {
        obj = Instantiate(obj, obj.transform.position, obj.transform.rotation);
        Vector2 temp = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0 + 45, Screen.width - 10), Random.Range(0 + Screen.height + 20, Screen.height + 50)));
        obj.transform.position = new Vector3(temp.x, temp.y, 0);
        }


    }
}
