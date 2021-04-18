using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManger : MonoBehaviour
{
   
    public Vector3[] m_spawnPos;
    public GameObject m_prebPlayer;
    public int m_playerCount;

    private void OnEnable()
    {
        global.g_playerCount = m_playerCount;
        PLayerSpawn(m_prebPlayer);
    }


    // Update is called once per frame
    void Update()
    {

    }
    private void PLayerSpawn(GameObject p) {

          

        if (m_playerCount <= m_spawnPos.Length)
        {
            for (int i = 0; i < m_playerCount; i++)
            {
                
                GameObject player = ObjectPool.me.GetObject(p, m_spawnPos[i], Quaternion.identity);
                global.PlayerItem currplayer;

                if (i == 0)
                {
                    global.g_mainPlayer = player;
                }

                currplayer.prebPlayer = player;
                currplayer.playerId = i;
                player.GetComponent<Player>().playerID = i;
                global.g_playerItems.Add(i, currplayer);
            }
        }
    }
}
