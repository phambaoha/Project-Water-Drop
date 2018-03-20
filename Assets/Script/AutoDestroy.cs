using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

	// Use this for initialization
    public float timeLive;
	void Start () {
        if (gameObject!= null)
        Destroy(gameObject, timeLive);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
