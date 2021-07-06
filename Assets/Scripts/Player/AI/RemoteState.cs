using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteState : FSMState
{
    private GameObject[] m_allPlayer;
    private GameObject m_targetPlayer;
    private float m_nextJumpTime;
    private float m_currTime;
    public RemoteState(FSMSystem fsm) : base(fsm)
    {
        stateID = StateID.ATTACK_2;
        m_targetPlayer = null;
        m_nextJumpTime = 5.0f;
        m_currTime = 0;
    }
    public override void Act(GameObject thisgo)
    {
        thisgo.GetComponent<PlayerControl>().Move(RemoteAttack(thisgo, 8.0f));
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

        if (thisgo.GetComponent<Player>().handisUseCount < 1)
        {
            fsm.PerformTransition(Transition.LOSE_WEAPON);
        }
        if (!WeaponTrunWeigth(thisgo)&& thisgo.GetComponent<Player>().handisUseCount >= 1)
        {
            fsm.PerformTransition(Transition.PREPARE_WEAPON_1);
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

    GameObject FindNearPlayer(GameObject thisgo)
    {
        m_allPlayer = GameObject.FindGameObjectsWithTag("Player");
        Vector3 thisgopos = thisgo.transform.position;
        GameObject mindistancego = null;
        float mindistance = 100.0f;
        if (m_allPlayer.Length != 1)
        {
            for (int i = 0; i < m_allPlayer.Length; i++)
            {
                if (m_allPlayer[i] == thisgo)
                {
                    continue;
                }

                float currdistance = Vector3.Distance(m_allPlayer[i].transform.position, thisgopos);
                if (currdistance < mindistance)
                {
                    mindistancego = m_allPlayer[i];
                }
            }
            return mindistancego;
        }

        return null;
    }
    float RemoteAttack(GameObject thisgo, float safedistance)
    {
        if (m_targetPlayer == null)
        {
            m_targetPlayer = FindNearPlayer(thisgo);
        }
        if (m_targetPlayer == null)
        {
            return 0;
        }
        if (!(m_targetPlayer.activeSelf))
        {
            m_targetPlayer = FindNearPlayer(thisgo);
        }
        if (m_targetPlayer == null)
        {
            return 0;
        }

        float targetX = m_targetPlayer.transform.position.x;
        float playerX = thisgo.transform.position.x;
 
   

        if ((targetX - safedistance) > playerX)
        {
    
            return -1.0f;
        }
        else if ((targetX + safedistance) < playerX)
        {
       
            return 1.0f;
        }
        else if((playerX > targetX && playerX < (targetX + safedistance)))
        {
   
            return -1.0f;
        }
        else if((playerX < targetX && playerX > (targetX - safedistance)))
        {
   
            return 1.0f;
        }
        else
        {
            return 0;
        }

    }
}