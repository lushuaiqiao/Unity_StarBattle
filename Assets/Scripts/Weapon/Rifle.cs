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
        this.gameObject.GetComponent<Collider2D>().isTrigger = false;
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        this.transform.parent = null;
        global.g_weaponCount--;
        Tips = null;
    }
    private void OnBecameVisible()
    {
        if (!isUse)
        {
            isVisible = true;
            Tips.SetActive(false);
        }


    }
    private void OnBecameInvisible()
    {
        if (!isUse)
        {
            isVisible = false;
            Tips.gameObject.SetActive(true);
        }
    }

}
