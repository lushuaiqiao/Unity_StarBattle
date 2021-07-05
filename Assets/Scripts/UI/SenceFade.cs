using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SenceFade : MonoBehaviour
{
    public float m_delayTimeIn;
    public float m_delayTimeOut;
    private float m_currTime;
    private Image m_fadeImage;
    private float m_a;
    private bool m_fadeout;
    private float addSub;

    void Start()
    {
        m_fadeImage = this.GetComponent<Image>();
        m_fadeImage.color = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
        m_a = 1.0f;
        m_fadeout = true;
    }
    private void OnEnable()
    {
        EventManager.me.AddEventListener("resetgame", (object[] o) => {
            m_fadeout = true;
            m_currTime = m_delayTimeOut;
            return null;
        });
        EventManager.me.AddEventListener("fadein", (object[] o) => {
            m_fadeout = false;
            m_currTime = m_delayTimeIn;
            return null;
        });
    }


    void Update()
    {
        FadeINrOUT(m_fadeout);
    }
    private void FadeINrOUT(bool isfadeout)
    {
        if (isfadeout)
        {

            if (m_a > 0)
            {
                m_currTime -= Time.deltaTime;
                if (m_currTime <= 0)
                {
                    m_a -= Time.deltaTime;
                    m_fadeImage.color = new Vector4(0.0f, 0.0f, 0.0f, m_a);
                }
            }
            else
            {
                m_a = 0;
            }
        }
        else
        {
            if (m_a < 1.0f)
            {
                m_currTime -= Time.deltaTime;
                if (m_currTime <= 0)
                {
                    m_a += Time.deltaTime;
                    m_fadeImage.color = new Vector4(0.0f, 0.0f, 0.0f, m_a);
                }
            }
            else
            {
                m_a = 1.0f;
            }
        }
    }

}
