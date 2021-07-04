using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDetails : MonoBehaviour
{
    public bool isrestart;
    public float lifeTime;

    private float m_currentTime;
    private bool m_isUse;

    CanvasGroup m_cg;
    private void Start()
    {
        m_isUse = false;
        isrestart = false;
        m_cg = this.GetComponent<CanvasGroup>();

    }

    private void Update()
    {
        if (isrestart)
        {
            m_isUse = true;
            m_currentTime = lifeTime;
            m_cg.alpha = 1.0f;
            isrestart = false;
        }
        if (m_isUse)
        {
            if (m_currentTime > 0.0f)
            {
                m_currentTime -= Time.deltaTime;
                if (m_currentTime > 0 && m_currentTime < 1.0f)
                {
               
                    m_cg.alpha -= Time.deltaTime;
                }
            }
            else
            {
                m_isUse = false;
            }
        }
    }

}
