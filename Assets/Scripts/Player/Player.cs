using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerID;
    public float playerHp;
  
    void OnEnable()
    {
        AfterCreate();
    }
    void Update()
    {
        if (playerHp <= 0)
        {
            PlayerDie();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    private void AfterCreate()
    {
        playerHp = 1000.0f;

    }
    private void PlayerDie()
    {
        BeforeDestroy();
        ObjectPool.me.PutObject(this.gameObject, 0);
    }
    private void BeforeDestroy() {
        global.g_playerItems.Remove(playerID);
    }
}
