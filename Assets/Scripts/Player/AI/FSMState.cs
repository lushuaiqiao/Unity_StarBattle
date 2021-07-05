using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Transition
{
    NULL_TRAN = 0,
    LOSE_WEAPON,
    PREPARE_WEAPON_1,
    PREPARE_WEAPON_2,
    HEALTH_LOSE
}

public enum StateID
{
    NULL_STATE = 0,
    FIND_WEAPON,
    ATTACK_1,
    ATTACK_2,
    ESCAPE
}
public abstract class FSMState
{
    protected StateID stateID;
    protected FSMSystem fsm;

    public FSMState(FSMSystem fsm)
    {
        this.fsm = fsm;
    }
    public StateID ID
    {
        get { return stateID; }
    }

    protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();

    public void AddTransition(Transition trans, StateID id)
    {
       
        if (trans == Transition.NULL_TRAN)
        {
            Debug.LogError("转换条件为空");
            return;
        }
        if (id == StateID.NULL_STATE)
        {
            Debug.LogError("状态为空");
            return;
        }
        if (map.ContainsKey(trans))
        {
            Debug.LogError("添加转换条件时，" + trans + "已经存在于map中");
            return;
        }

        map.Add(trans, id);

    }

    public void DeleteTransition(Transition trans)
    {
        if (trans == Transition.NULL_TRAN)
        {
            Debug.LogError("转换条件为空");
            return;
        }
        if (map.ContainsKey(trans) == false)
        {
            Debug.LogError("添加转换条件时，" + trans + "不存在于map中");
            return;
        }

        map.Remove(trans);
    }

    public StateID GetOutputState(Transition trans)
    {
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }

        return StateID.NULL_STATE;
    }

    public virtual void DoBeforEntering() { }
    public virtual void DoAfterLeaving() { }
    public abstract void Act(GameObject npc);
    public abstract void Reason(GameObject npc);



}