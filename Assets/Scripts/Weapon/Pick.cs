using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pick : MonoBehaviour
{
    private Transform weapon;
    private GameObject details;
    private Text textName;
    private Text textDetails;
    private void OnEnable()
    {

        textName = GameObject.Find("Canvas/Weapon details/Name").GetComponent<Text>();
        textDetails = GameObject.Find("Canvas/Weapon details/Details").GetComponent<Text>();

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
                weapon = this.transform.parent;
                weapon.GetComponent<Weapon>().isUse = true;
                weapon.GetComponent<Weapon>().userId = collision.transform.parent.GetComponent<Player>().playerID;
                textName.text = weapon.GetComponent<Weapon>().strName;
                textDetails.text = weapon.GetComponent<Weapon>().strDetail;
                textName.transform.parent.GetComponent<WeaponDetails>().isrestart = true;

                this.gameObject.GetComponent<Collider2D>().enabled = false;
                this.transform.parent.parent = collision.transform;
            }
        }
    }
}
