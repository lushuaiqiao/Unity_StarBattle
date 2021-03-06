using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCreate : MonoBehaviour
{
    public float m_createRangeMax;
    public float m_createRangeMin;
    public float m_createSpeed;
    public float m_createMax;
    public float m_createCount;
    public float m_createHeight;
    public bool m_isWork;
    public GameObject[] weaponList;

    private float m_localTime;
    private Transform m_LocalTransform;
    private void Start()
    {
        m_localTime = 0;
        m_isWork = true;
        m_LocalTransform = this.transform;
    }
    private void FixedUpdate()
    {
        if (global.g_weaponCount < m_createMax && global.g_isWeaponCreate)
        {
            m_isWork = true;
        }
        else
        {
            m_isWork = false;
        }

        m_createCount = global.g_weaponCount;



        if (m_isWork)
        {
            m_localTime += Time.deltaTime;
            if (m_localTime >= m_createSpeed)
            {
                m_localTime = 0;
                SpawnWeapon();
            }
        }
    }
    private void SpawnWeapon() {
        float randomX = Random.Range(m_createRangeMin, m_createRangeMax);
        Vector3 spawnPos = new Vector3(randomX, m_LocalTransform.position.y + m_createHeight, m_LocalTransform.position.z);

        int weaponType = Random.Range(0, weaponList.Length);
        GameObject weapon = ObjectPool.me.GetObject(weaponList[weaponType], spawnPos, Quaternion.identity);
        global.g_weaponCount++;
    }

}
