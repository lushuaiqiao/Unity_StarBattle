using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int playerID;
    public int handisUseCount;
    [HideInInspector]

    public float maxHp = 30.0f;

    [HideInInspector]
    public Dictionary<int, float> playerHand = new Dictionary<int, float>();
    [HideInInspector]
    public float playerHp;
    [HideInInspector]
    public Dictionary<int, GameObject> HandWeapon = new Dictionary<int, GameObject>();

    void OnEnable()
    {
        AfterCreate();
        EventManager.me.AddEventListener("endgame", (object[] o) => { playerHp = 0; return null; });
    }
    private void OnDisable()
    {
        EventManager.me.RemoveEventListener("endgame", (object[] o) => { playerHp = 0; return null; });
    }
    void Update()
    {
        if (playerHp <= 0)
        {
            PlayerDie();
        }
    }

    private void AfterCreate()
    {
        for (int i = 0; i < 5; i++)
        {
            if (playerHand.ContainsKey(i))
            {
                playerHand[i] = 0; 
            }
            else
            {
                playerHand.Add(i, 0);
            }
        }
        playerHp = maxHp;
    }
    private void BeforeDestroy()
    {
        global.g_leftPlayerId.Remove(playerID);
        global.g_playerCount--;
        playerHp = maxHp;
        foreach (var item in HandWeapon)
        {
            item.Value.transform.GetComponent<Weapon>().DestroyObject();
        }
    }
    private void PlayerDie()
    {
        BeforeDestroy();
        ObjectPool.me.PutObject(this.gameObject, 0);
    }
    public void BeHit(float damage)
    {
        playerHp -= damage;
    }

}
