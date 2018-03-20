using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoremove : MonoBehaviour
{

    // Use this for initialization
    public Transform textscore;
    public  bool isaddscore;

    public static scoremove sm;
  
    void Awake()
    {
        sm = this;
    }
    // Update is called once per frame
    void Update()
    {

       // transform.position = new Vector3(Mathf.Lerp(transform.position.x,textscore.transform.position.x,5*Time.deltaTime),Mathf.Lerp(transform.position.y,textscore.transform.position.y,5*Time.deltaTime),0);
        if (transform.position.x >= -6f && transform.position.x <=-4f)
            transform.Translate(new Vector3(0, 9, 0) * Time.deltaTime);
        else
            if (transform.position.x > -4 && transform.position.x <= -1)
                transform.Translate(new Vector3(0, 8, 0) * Time.deltaTime);
            else
                    if(transform.position.x >-1 && transform.position.x < 3.2f)
                        transform.Translate(new Vector3(-4, 8, 0) * Time.deltaTime );
            else if(transform.position.x >=3.2 )
                transform.Translate(new Vector3(-4, 6, 0) * Time.deltaTime );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Score")
        {
            isaddscore = true;
            Destroy(gameObject);
        }
        else
        {
            isaddscore = false;
        }
    }
}
