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
        AfterCreate();
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUse)
        {
            if (collision.tag == "Player")
            {
                int id = collision.GetComponent<Player>().playerID;
                if (userId != id)
                {
                    collision.GetComponent<Player>().playerHp -= damage;
                    BeforeDestroy();
                    ObjectPool.me.PutObject(this.gameObject, 0);
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
}
