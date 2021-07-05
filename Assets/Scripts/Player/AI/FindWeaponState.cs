using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindWeaponState : FSMState
{
    private GameObject m_targetWeapon;
    private GameObject[] m_allWeapon;
    private float m_nextJumpTime;
    private float m_currTime;
    public FindWeaponState(FSMSystem fsm) : base(fsm)
    {
        stateID = StateID.FIND_WEAPON;
        m_targetWeapon = null;
        m_nextJumpTime = 5.0f;
        m_currTime = 0;
    }

    public override void Act(GameObject thisgo)
    {
        thisgo.GetComponent<PlayerControl>().Move(FindAction(thisgo));
        m_currTime += Time.deltaTime;
        if (m_currTime >= m_nextJumpTime)
        {
            m_currTime = 0;
            m_nextJumpTime = Random.Range(5.0f, 10.0f);
            thisgo.GetComponent<PlayerControl>().Jump();
        }

    }

    public override void Reason(GameObject thisgo)
    {

        if (thisgo.GetComponent<Player>().handisUseCount > 1)
        {
            if (WeaponTrunWeigth(thisgo))
            {
                fsm.PerformTransition(Transition.PREPARE_WEAPON_2);
            }
            else
            {
                fsm.PerformTransition(Transition.PREPARE_WEAPON_1);
            }
        }

    }

    bool WeaponTrunWeigth(GameObject thisgo)
    {
        float weightSum = 0;
        for (int i = 0; i < 5; i++)
        {
            if (thisgo.GetComponent<Player>().playerHand.ContainsKey(i))
            {
                weightSum += thisgo.GetComponent<Player>().playerHand[i];
            }
            else
            {
                continue;
            }

        }
  
        if (weightSum < 0)
        {

            return true;
        }
        else
        {

            return false;
        }
    }
    private  GameObject FindNearWeapon(GameObject thisgo)
    {
        m_allWeapon = GameObject.FindGameObjectsWithTag("Weapon");
        Vector3 thisgopos = thisgo.transform.position;
        GameObject mindistancego = null;
        float mindistance = 100.0f;

        if (m_allWeapon.Length > 0)
        {
            for (int i = 0; i < m_allWeapon.Length; i++)
            {
                if (m_allWeapon[i].GetComponent<Weapon>().isUse)
                {
                    continue;
                }
       
                float currdistance = Vector3.Distance(m_allWeapon[i].transform.position, thisgopos);
                if (currdistance < mindistance)
                {
                    mindistance = currdistance;
                    mindistancego = m_allWeapon[i];
                }
            }
            return mindistancego;
        }

        return null;
    }
  private  float FindAction(GameObject thisgo) {

        if (m_targetWeapon == null)
        {
            m_targetWeapon = FindNearWeapon(thisgo);
        }
        if (m_targetWeapon == null)
        {
            return 0;
        }

        if (m_targetWeapon.GetComponent<Weapon>().isUse)
        {
            m_targetWeapon = FindNearWeapon(thisgo);
        }

        if (m_targetWeapon == null)
        {
            return 0;
        }


        if (m_targetWeapon.transform.position.x > thisgo.transform.position.x)
        {
            return -1.0f;
        }
        else
        {
            return 1.0f;
        }
    }
}
