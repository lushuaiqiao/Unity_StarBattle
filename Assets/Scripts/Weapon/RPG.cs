using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : Weapon
{
    [SerializeField]
    private float m_initLifeTime = 20.0f;
    private GameObject Tips;
    private void Start()
    {
        strName = "火箭筒";
        strDetail = "可以发射子弹 伤害高";
    }
    void OnEnable()
    {
        EventManager.me.AddEventListener("endgame", (object[] o) => {
            isUse = true;
            lifeTime = 0;
      
            return null;
        });
        AfterCreate();
    }
    private void OnDisable()
    {
        EventManager.me.RemoveEventListener("endgame", (object[] o) => {
            isUse = true;
            lifeTime = 0;
     
            return null;
        });
        BeforeDestroy();
    }

    void Update()
    {

        if (isUse)
        {
            Tips.SetActive(false);
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                DestroyObject();
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
   
        Tips = transform.Find("Tips").gameObject;
    }
    public override void BeforeDestroy()
    {
        isUse = false;
        isBorn = false;
        isLand = false;

        this.transform.parent = null;


        Tips = null;
    }
    public override void DestroyObject()
    {
        BeforeDestroy();
        ObjectPool.me.PutObject(this.gameObject, 0);
    }
    private void OnBecameVisible()
    {
        if ((!isUse) && isBorn)
        {
            isVisible = true;
            Tips.SetActive(false);
        }
        else if (isUse && isBorn)
        {
            isVisible = true;
            Tips.SetActive(false);
        }

    }
    private void OnBecameInvisible()
    {
        if ((!isUse) && isBorn)
        {
            isVisible = false;
            Tips.SetActive(true);
        }

    }


}
