using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    private Text text;
    private float m_a;
    private float addSub;
   

    void Start()
    {
        text = this.GetComponent<Text>();
 
    }
    private void OnEnable()
    {
        addSub = -1.0f;
    }

    void Update()
    {
        text.color= new Vector4(0.0f, 0.0f, 0.0f, m_a);
        if (m_a>1.0f)
        {
            addSub = -1.0f;
        }
        if (m_a<0)
        {
            addSub = 1.0f;
        }
        m_a += addSub * Time.deltaTime;
    }
}
