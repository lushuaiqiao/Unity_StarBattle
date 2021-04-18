using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Player
{
    public float m_rotateSpeed = 0;
    public float m_maxSpeed = 0;
    public float m_jumpSpeed = 0;

    private float m_currentSpeed = 0;
    private Rigidbody2D m_rigidbody;
    public bool m_isLand = false;
    private Player m_mainPlayer;

    private float m_axis = 0;
    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        m_mainPlayer = global.g_mainPlayer.GetComponent<Player>();
    }
    void LateUpdate()
    {
        if (m_mainPlayer.playerID==playerID)
        {
            Move();
            Jump();
        }
    }
    private void Move()
    {

        //旋转移动
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if (!(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)))
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Turn(1.0f);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    Turn(-1.0f);

                }
            }
        }
        else
        {
            m_axis = m_rigidbody.transform.localEulerAngles.z;
            m_currentSpeed = 0;
      
        }
    }
    private void Jump()
    {
        //跳跃
        if (m_isLand)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_isLand = false;
                m_rigidbody.AddForce(Vector2.up * m_jumpSpeed);
                
               
            }
        }
    }
    private void Turn(float inputValue)
    {
        m_currentSpeed += m_rotateSpeed * Time.deltaTime * inputValue;

        if (m_currentSpeed > m_maxSpeed)
        {
            m_currentSpeed = m_maxSpeed;
        }
        if (m_currentSpeed < (m_maxSpeed * -1.0f))
        {
            m_currentSpeed = (m_maxSpeed * -1.0f);
        }

        m_axis += m_currentSpeed * Time.deltaTime;
        m_rigidbody.MoveRotation(m_axis);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Underground")
        {
            m_isLand = true;
        }
    }
}
