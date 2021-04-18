using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : Player
{
    public bool m_isUse = false;
    private float m_coolDown = 0;

    private void Update()
    {
        if (m_isUse)
        {
            m_coolDown -= Time.deltaTime;
            if (m_coolDown <= 0)
            {
                m_isUse = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!m_isUse)
        {
            if (collision.tag == "Pick")
            {
                m_isUse = true;
                m_coolDown = collision.transform.parent.GetComponent<Weapon>().lifeTime;
            }
        }

    }
}
