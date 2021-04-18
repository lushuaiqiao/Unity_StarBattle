using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
    public Camera m_camera;
    public float m_space;

    private Weapon m_weapon;
    private float m_halfWidth;
    private float m_halfHeight;
    private GameObject m_weaponBlack;


    void OnEnable()
    {
        m_camera = global.g_mainCamera;
        m_weapon = this.transform.parent.GetComponent<Weapon>();
        HeightWidthCalculate();
        m_weaponBlack = this.transform.Find("Weapon_black").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        SetPostion();
    }

    private void HeightWidthCalculate()
    {
        Vector3 upperright = m_camera.ViewportToWorldPoint(new Vector2(1, 0));
        m_halfWidth = upperright.x - m_camera.transform.position.x;
        m_halfHeight = upperright.y - m_camera.transform.position.y;
    }
    private void SetPostion() {

        if (!m_weapon.isUse && !m_weapon.isVisible)
        {
            float x, y;

            if (m_weapon.transform.position.x > m_camera.transform.position.x + m_halfWidth)
            {
                x = m_camera.transform.position.x + m_halfWidth- m_space;
   
                this.transform.rotation = Quaternion.Euler(0, 0, 0.0f);
                m_weaponBlack.transform.localRotation = Quaternion.Euler(0, 0, 225.0f);


            }
            else if (m_weapon.transform.position.x < m_camera.transform.position.x - m_halfWidth)
            {
                x = m_camera.transform.position.x - m_halfWidth+ m_space;
      
                this.transform.rotation = Quaternion.Euler(0, 0, 180.0f);
                m_weaponBlack.transform.localRotation = Quaternion.Euler(0, 0, 45.0f);


            }
            else
            {
                x = m_weapon.transform.position.x;
            }


            if (m_weapon.transform.position.y > m_camera.transform.position.y + m_halfWidth)
            {
                y = m_camera.transform.position.y + m_halfWidth- m_space;
            }
            if (m_weapon.transform.position.y < m_camera.transform.position.y - m_halfWidth)
            {
                y = m_camera.transform.position.y - m_halfWidth+ m_space;
            }
            else
            {
                y = m_weapon.transform.position.y;
            }

            this.transform.position = new Vector2(x, y);
        }
    }
}
