using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : Player
{
    public int handId;
    public bool m_isUse = false;
    private float m_coolDown = 0;
    private Player m_currPlayer;

    private void OnEnable()
    {
        m_coolDown = 0;
        m_isUse = false;
        this.gameObject.GetComponent<Collider2D>().enabled = true;
        m_currPlayer = this.transform.parent.GetComponent<Player>();
    }
    private void OnDisable()
    {
        m_coolDown = 0;
        m_isUse = false;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        if (this.transform.childCount!=0)
        {
            this.transform.DetachChildren();
        }
    }

    private void Update()
    {
        if (m_isUse)
        {
            m_coolDown -= Time.deltaTime;
            if (m_coolDown <= 0)
            {
                m_isUse = false;

                if (m_currPlayer.playerHand.ContainsKey(handId))
                {
                    m_currPlayer.playerHand[handId] = 0;
                }
                else
                {
                    m_currPlayer.playerHand.Add(handId, 0);
                }
                this.transform.parent.GetComponent<Player>().handisUseCount--;
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
              
                Weapon currweapom = collision.transform.parent.GetComponent<Weapon>();
                m_coolDown = currweapom.lifeTime;
                m_currPlayer.handisUseCount++;
                if (m_currPlayer.playerHand.ContainsKey(handId))
                {
                    m_currPlayer.playerHand[handId] = currweapom.AIWeight;
                }
                else
                {
                    m_currPlayer.playerHand.Add(handId, currweapom.AIWeight);
                }
                if (m_currPlayer.HandWeapon.ContainsKey(handId))
                {
                    m_currPlayer.HandWeapon[handId] = collision.transform.parent.gameObject;
                }
                else
                {
                    m_currPlayer.HandWeapon.Add(handId, collision.transform.parent.gameObject);
                }
          
            }
        }

    }
}
