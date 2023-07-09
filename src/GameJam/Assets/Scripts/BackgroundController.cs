using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour{

    public int fase; //1- 1 e 2; 2- 3 e 4; 3- 5 e 6; 4- 7 e 8

    void Awake(){
        gameObject.GetComponent<Animator>().SetInteger("fase",fase);
    }
}
