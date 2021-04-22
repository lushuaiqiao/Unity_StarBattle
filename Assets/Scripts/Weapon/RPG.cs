using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : Weapon
{
    [SerializeField]
    private float m_initLifeTime = 20.0f;
    private GameObject Tips;
    void OnEnable()
    {
        AfterCreate();
    }

    void Update()
    {

        if (isUse)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                BeforeDestroy();
                ObjectPool.me.PutObject(this.gameObject, 0);
            }

        }
        else
        {
            Drop(!isLand);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isUse)
        {
            if (collision.tag == "Land")
            {
                isLand = true;
            }
        }
    }

    public override void AfterCreate()
    {
        name = "PrebRPG";
        isBorn = true;
        isUse = false;
        lifeTime = m_initLifeTime;
        global.g_weaponCount++;
        Tips = transform.Find("Tips").gameObject;
    }
    public override void BeforeDestroy()
    {
        isUse = false;
        isBorn = false;
        isLand = false;
        //this.gameObject.GetComponent<Collider2D>().isTrigger = false;
        //this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        this.transform.parent = null;
     
        Tips = null;
    }
    private void OnBecameVisible()
    {
        if (!isUse && isBorn)
        {
            isVisible = true;
            Tips.SetActive(false);
        }


    }
    private void OnBecameInvisible()
    {
        if (!isUse && isBorn)
        {
            isVisible = false;
            Tips.gameObject.SetActive(true);
        }
        else if (isUse)
        {
            isVisible = false;
            Tips.SetActive(false);
        }
    }


}
