using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamControl : MonoBehaviour
{
    public float m_dampTime = 0.2f;
    public Transform mainPlayerPos;
    
    private Vector2 m_cameraPos;
    private Vector3 m_moveVelocity;

    void Start()
    {
        global.g_mainCamera = this.GetComponent<Camera>();
        mainPlayerPos = global.g_mainPlayer.transform;
        m_cameraPos = new Vector2(0, 0);
    }


    void LateUpdate()
    {
   
        if (mainPlayerPos != null)
        {
            FollowMain();
        }
        else
        {
            this.transform.position = new Vector3(0, 0, -5.0f);
        }
    }

    void FollowMain()
    {
        if (m_cameraPos.y > mainPlayerPos.position.y+3.0f)
        {
            m_cameraPos.y = mainPlayerPos.position.y + 3.0f;
        }
        else if (m_cameraPos.y < mainPlayerPos.position.y - 3.0f)
        {
            m_cameraPos.y = mainPlayerPos.position.y - 3.0f;
        }
        Vector3 target =  new Vector3(mainPlayerPos.position.x, m_cameraPos.y, -5.0f);
        this.transform.position = Vector3.SmoothDamp(this.transform.position, target, ref m_moveVelocity, m_dampTime);

    }


}
