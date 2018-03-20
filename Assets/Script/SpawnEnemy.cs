using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

	// Use this for initialization
    public GameObject[] enemy;

	void Start () {

        InvokeRepeating("Spawn", 5f, 5f);
	}
	
    void Spawn()
    {
        GameObject obj = enemy[Random.Range(0, enemy.Length)];
        obj = Instantiate(obj, obj.transform.position, transform.rotation);
        Vector2 temp = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0 + 20,Screen.width -20),Screen.height + 30));
        obj.transform.position = new Vector3(temp.x, temp.y, 0);
    }
	
}
