using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamControl : MonoBehaviour
{

    private Transform mainPlayerPos;
    private float m_dampTime = 0.2f;
    private GameObject m_targetGameObject;
    private Vector2 m_cameraPos;
    private Vector3 m_moveVelocity;

    void OnEnable()
    {
        global.g_mainCamera = this.GetComponent<Camera>();
        m_cameraPos = new Vector2(0, 0);
        m_targetGameObject = null;
    }

    void LateUpdate()
    {
        if (m_targetGameObject == null)
        {
           ChangeCarmeraTarget();
        }
        else
        {
            if (!m_targetGameObject.activeSelf)
            {
                ChangeCarmeraTarget();
            }
        }

        FollowMain();
    }

    private void FollowMain()
    {
        if (m_cameraPos.y > mainPlayerPos.position.y + 3.0f)
        {
            m_cameraPos.y = mainPlayerPos.position.y + 3.0f;
        }
        else if (m_cameraPos.y < mainPlayerPos.position.y - 3.0f)
        {
            m_cameraPos.y = mainPlayerPos.position.y - 3.0f;
        }
        Vector3 target = new Vector3(mainPlayerPos.position.x, m_cameraPos.y, -5.0f);
        this.transform.position = Vector3.SmoothDamp(this.transform.position, target, ref m_moveVelocity, m_dampTime);
    }
    private GameObject FindCarmeraTarget()
    {
        GameObject[] allplayer = GameObject.FindGameObjectsWithTag("Player");
        GameObject targetplayer = null;
        if (allplayer.Length<=0)
        {
            return null;
        }
        for (int i = 0; i < allplayer.Length; i++)
        {
            targetplayer = allplayer[i];
            if (allplayer[i].GetComponent<Player>().playerID==0)
            {
                return targetplayer;
            }

        }
        return targetplayer;

    }
    private void ChangeCarmeraTarget() {

        if ((m_targetGameObject = FindCarmeraTarget()) != null)
        {
            if (mainPlayerPos != m_targetGameObject.transform)
            {
                mainPlayerPos = m_targetGameObject.transform;
            }
        }
        else
        {
            mainPlayerPos.position = new Vector3(0, 0, -5.0f);
        }

    }

}
