using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObj : MonoBehaviour
{

    public GameObject obj;
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = transform.position.z;
            Instantiate(obj, pos,Quaternion.identity);
        }
    }
}
