using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector]
    public int userId;
    [HideInInspector]
    public bool isBorn = false;
    [HideInInspector]
    public bool isUse = false;
    [HideInInspector]
    public float lifeTime = 0;
    [HideInInspector]
    public float damage = 0;
    [HideInInspector]
    public Vector3 bornPos = Vector3.zero;
    [HideInInspector]
    public float fallSpeed = 0;
    [HideInInspector]
    public bool isVisible = false;
    [HideInInspector]
    public bool isLand = false;



    public virtual void AfterCreate(){}
    public virtual void BeforeDestroy(){}

    public void Drop(bool isdrop) {
        if (isdrop)
        {
            this.transform.position += new Vector3(0.0f,-5.0f*Time.deltaTime,0.0f);
        }
    }

}
