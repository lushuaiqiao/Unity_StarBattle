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

   

    public virtual void AfterCreate(){}
    public virtual void BeforeDestroy(){}

}
