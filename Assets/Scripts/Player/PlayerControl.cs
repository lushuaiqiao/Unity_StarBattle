using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Player
{
    public float rotateSpeed = 0;
    public float maxSpeed = 0;
    public float jumpSpeed = 0;

    private float m_currentSpeed = 0;
    private Rigidbody2D m_rigidbody;
    private bool m_isLand = false;

    private float m_axis = 0;
    private FSMSystem fsm;
    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();

    }
    private void OnEnable()
    {

        InitFSM();

    }

    void LateUpdate()
    {
        if (playerID == 0)
        {
            MainPlayerInput();
        }
        else
        {
            fsm.Update(this.gameObject);
        }

    }
    public void Move(float direction)
    {
        if (direction == 0)
        {
            m_axis = m_rigidbody.transform.localEulerAngles.z;
            m_currentSpeed = 0;
        }
        else
        {
            Turn(direction);
        }
    }
    public void Jump()
    {
    
        if (m_isLand)
        {
            m_isLand = false;
            m_rigidbody.AddForce(Vector2.up * jumpSpeed);
        }
    }
    private void MainPlayerInput()
    {

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if (!(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)))
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Move(1.0f);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    Move(-1.0f);

                }
            }
        }
        else
        {
            Move(0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    private void Turn(float inputValue)
    {
        m_currentSpeed += rotateSpeed * Time.deltaTime * inputValue;

        if (m_currentSpeed > maxSpeed)
        {
            m_currentSpeed = maxSpeed;
        }
        if (m_currentSpeed < (maxSpeed * -1.0f))
        {
            m_currentSpeed = (maxSpeed * -1.0f);
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

    void InitFSM()
    {
        fsm = new FSMSystem();

        FSMState Melee = new MeleeState(fsm);
        Melee.AddTransition(Transition.LOSE_WEAPON, StateID.FIND_WEAPON);
        Melee.AddTransition(Transition.PREPARE_WEAPON_2, StateID.ATTACK_2);

        FSMState Remote = new RemoteState(fsm);
        Remote.AddTransition(Transition.LOSE_WEAPON, StateID.FIND_WEAPON);
        Remote.AddTransition(Transition.PREPARE_WEAPON_1, StateID.ATTACK_1);

        FSMState FindWeapon = new FindWeaponState(fsm);
        FindWeapon.AddTransition(Transition.PREPARE_WEAPON_1, StateID.ATTACK_1);
        FindWeapon.AddTransition(Transition.PREPARE_WEAPON_2, StateID.ATTACK_2);

        fsm.AddState(FindWeapon);
        fsm.AddState(Melee);
        fsm.AddState(Remote);
    }
}
