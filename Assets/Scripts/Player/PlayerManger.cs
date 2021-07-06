using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManger : MonoBehaviour
{
   
    public Vector3[] spawnPos;
    public GameObject prebPlayer;
    public int playerCount;
    private bool m_isCreate;

    private void OnEnable()
    {
        EventManager.me.AddEventListener("resetgame", (object[] o) =>{ m_isCreate = true; return null;});
    }
    private void OnDisable()
    {
        EventManager.me.RemoveEventListener("resetgame", (object[] o) => { m_isCreate = true; return null; });
    }
    private void Update()
    {
        if (m_isCreate)
        {
            PLayerSpawn(prebPlayer);
            m_isCreate = false;
        }
        
    }

    private void PLayerSpawn(GameObject p)
    {
        for (int i = 0; i < playerCount; i++)
        {
            GameObject player = ObjectPool.me.GetObject(p, spawnPos[i], Quaternion.identity);
            global.g_playerCount = playerCount;
            player.GetComponent<Player>().playerID = i;
            player.GetComponent<Player>().playerHp = 30.0f;
            player.GetComponent<Player>().handisUseCount = 0;
    
           global.g_leftPlayerId.Add(i);
            if (i == 0)
            {
                global.g_mainPlayer = player;
            }

        }

    }
}
