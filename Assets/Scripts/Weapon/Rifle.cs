using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField]
    private float m_initLifeTime = 20.0f;
    [SerializeField]
    private float m_initDamage = 2.0f;

    private GameObject Tips;
    private void Start()
    {
        strName = "步枪";
        strDetail = "可以发射子弹 伤害中等";
    }
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
        name = "PrebRifle";
        isBorn = true;
        isUse = false;
        lifeTime = m_initLifeTime;
        damage = m_initDamage;
        global.g_weaponCount++;
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
