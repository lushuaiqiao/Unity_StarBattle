using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSystem
{
    private Dictionary<StateID, FSMState> m_state = new Dictionary<StateID, FSMState>();
    private StateID m_currentStateID;
    private FSMState m_currentState;

    public void Update(GameObject npc)
    {
        m_currentState.Act(npc);
        m_currentState.Reason(npc);
    }

    public void AddState(FSMState s)
    {
        if (s == null)
        {
            Debug.LogError("state为空");
            return;
        }

        if (m_currentState == null)
        {
            m_currentState = s;
            m_currentStateID = s.ID;
        }

        if (m_state.ContainsKey(s.ID))
        {
            Debug.LogError("状态" + s.ID + "已经存在，无法重复添加");
            return;
        }

        m_state.Add(s.ID, s);
    }

    public void DeleteFSMState(StateID id)
    {
        if (id == StateID.NULL_STATE)
        {
            Debug.LogError("无法删除空状态");
            return;
        }

        if (m_state.ContainsKey(id) == false)
        {
            Debug.LogError("无法删除不存在状态" + id);
            return;
        }

        m_state.Remove(id);
    }

    public void PerformTransition(Transition trans)
    {
        if (trans == Transition.NULL_TRAN)
        {
            Debug.LogError("无法执行空的转换条件");
            return;
        }
        StateID id = m_currentState.GetOutputState(trans);
        if (id == StateID.NULL_STATE)
        {
            Debug.LogWarning("当前状态" + m_currentStateID + "无法根据转换条件" + trans + "发生转换");
            return;
        }

        if (m_state.ContainsKey(id) == false)
        {
            Debug.LogError("状态机内没有包含" + id + "，无法进行状态转换");
            return;
        }

        FSMState state = m_state[id];
        m_currentState.DoAfterLeaving();
        m_currentState = state;
        m_currentStateID = id;
        m_currentState.DoBeforEntering();
    }
}