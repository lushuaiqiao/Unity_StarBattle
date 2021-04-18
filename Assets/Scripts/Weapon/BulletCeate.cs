using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCeate:MonoBehaviour
{
    public GameObject m_bulletType;
    public float m_shootSpeed;

    private float m_localTime = 0;
    private GameObject m_bullet;

    private void OnEnable()
    {

    }

    void FixedUpdate()
    {
        if (this.transform.parent.GetComponent<Weapon>().isUse)
        {
            m_localTime += Time.deltaTime;
            if (m_localTime >=m_shootSpeed)
            {
                m_localTime = 0;
                m_bullet = ObjectPool.me.GetObject(m_bulletType, this.transform.position, Quaternion.identity);
                m_bullet.GetComponent<Weapon>().userId = this.transform.parent.GetComponent<Weapon>().userId;
                m_bullet.GetComponent<Rigidbody2D>().velocity = (-(this.transform.right) * 5);
                m_bullet.transform.rotation =  this.transform.rotation;
            }

        }
 
    }

}
