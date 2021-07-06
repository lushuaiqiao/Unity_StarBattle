using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheild : Weapon
{
    [SerializeField]
    private float m_sheildPower = 500.0f;
    [SerializeField]
    private float m_initLifeTime = 20.0f;

    private GameObject Tips;

    private void Start()
    {
        strName = "盾牌";
        strDetail = "阻挡正面的武器和子弹 产生反震和反弹效果";
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
        if (isUse)
        {
            if (collision.tag == "Weapon")
            {
                Weapon Weapon = collision.GetComponent<Weapon>();
                if (userId != Weapon.userId&&Weapon.isUse)
                {
                    Vector2 F = (this.transform.position - this.transform.parent.parent.position).normalized;
                    collision.transform.parent.parent.GetComponent<Rigidbody2D>().AddForce(F * m_sheildPower);
                }
            }
            if (collision.tag == "Bullet")
            {
                int id = collision.GetComponent<Weapon>().userId;
                if (userId != id)
                {
                    collision.GetComponent<Weapon>().userId = userId;


                    Vector2 F = (-this.transform.up).normalized;

                    Vector2 V = collision.GetComponent<Rigidbody2D>().velocity;
                    F = Vector2.Reflect(V, F);
                    collision.GetComponent<Rigidbody2D>().velocity = F;
                    float angle = Vector3.Angle(V, F);
                    collision.transform.eulerAngles += new Vector3(0,0,angle);


                }
            }
        }
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
        name = "PreSheild";
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
