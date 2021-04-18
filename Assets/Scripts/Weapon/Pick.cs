using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    private Transform weapon;
    private void OnEnable()
    {
     
        this.gameObject.GetComponent<Collider2D>().enabled = true;
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hand")
        {
            Debug.Log("1");
            if (!collision.GetComponent<PlayerPickUp>().m_isUse)
            {
                Debug.Log("2");
                weapon = this.transform.parent;
                weapon.GetComponent<Weapon>().isUse = true;
                weapon.GetComponent<Weapon>().userId = collision.transform.parent.GetComponent<Player>().playerID;
                weapon.GetComponent<Collider2D>().isTrigger = true;
                weapon.GetComponent<Rigidbody2D>().isKinematic = true;
                weapon.GetComponent<Rigidbody2D>().Sleep();

                this.gameObject.GetComponent<Collider2D>().enabled = false;
                this.transform.parent.parent = collision.transform;
            }
        }
    }
}
