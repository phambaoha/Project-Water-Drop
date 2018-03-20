using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballgroup : MonoBehaviour {

    public static ballgroup ballGroup= new ballgroup();
    public List<BallController> ball;
    private ballgroup()
    {
        ballGroup = this;
    }
  

}
