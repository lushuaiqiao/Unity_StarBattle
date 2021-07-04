using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pick : MonoBehaviour
{
    private Transform m_weapon;
    private GameObject m_details;
    private Text m_textName;
    private Text m_textDetails;
    private void OnEnable()
    {

        m_textName = GameObject.Find("Canvas/Weapon details/Name").GetComponent<Text>();
        m_textDetails = GameObject.Find("Canvas/Weapon details/Details").GetComponent<Text>();

        this.gameObject.GetComponent<Collider2D>().enabled = true;
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hand")
        {
            if (!collision.GetComponent<PlayerPickUp>().m_isUse)
            {
                global.g_weaponCount--;
                m_weapon = this.transform.parent;
                m_weapon.GetComponent<Weapon>().isUse = true;
                m_weapon.GetComponent<Weapon>().userId = collision.transform.parent.GetComponent<Player>().playerID;

                if (m_weapon.GetComponent<Weapon>().userId == 0) {
                    m_textName.text = m_weapon.GetComponent<Weapon>().strName;
                    m_textDetails.text = m_weapon.GetComponent<Weapon>().strDetail;
                    m_textName.transform.parent.GetComponent<WeaponDetails>().isrestart = true;
                }
            
                this.gameObject.GetComponent<Collider2D>().enabled = false;
                this.transform.parent.parent = collision.transform;
            }
        }
    }
}
