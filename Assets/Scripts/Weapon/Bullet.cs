using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Weapon
{
    [SerializeField]
    private float m_initLifeTime = 20.0f;
    [SerializeField]
    private float m_initDamage = 2.0f;
    void OnEnable()
    {
        EventManager.me.AddEventListener("endgame", (object[] o) => {
            DestroyObject();
            return null;
        });
        AfterCreate();
    }
    private void OnDisable()
    {
        EventManager.me.RemoveEventListener("endgame", (object[] o) => {
            DestroyObject();
            return null;
        });
    }

    void Update()
    {

        if (isUse)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                isUse = false;

                ObjectPool.me.PutObject(this.gameObject, 0);
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isUse)
        {
            if (collision.tag == "Player")
            {
                int id = collision.GetComponent<Player>().playerID;
                if (userId != id)
                {
                    collision.GetComponent<Player>().BeHit(damage);
                    DestroyObject();
                }
            }
        }
    }
    public override void AfterCreate()
    {
        name = "bullet";
        isBorn = true;
        isUse = true;
        lifeTime = m_initLifeTime;
        damage = m_initDamage;
    }
    public override void BeforeDestroy()
    {
        isUse = false;
    }

    public override void DestroyObject()
    {
        BeforeDestroy();
        ObjectPool.me.PutObject(this.gameObject, 0);
    }
}
