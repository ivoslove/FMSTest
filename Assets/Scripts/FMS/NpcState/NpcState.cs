using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NpcState
{
    protected Dictionary<NpcStateTransition, NpcStateCode> transitionDictionary=new Dictionary<NpcStateTransition, NpcStateCode>();

    protected NpcStateCode npcStateCode;

    protected NpcFMSystem fms;

    protected Character charactor;

    public NpcStateCode NpcStateCode
    {
        get { return npcStateCode; }
    }

    public NpcState(NpcFMSystem npcFMS,Character charactor)
    {
        this.fms = npcFMS;
        this.charactor = charactor;
    }

    public void AddTransition(NpcStateTransition transition,NpcStateCode code)
    {
        if (transition.Equals(NpcStateTransition.NullTransition))
        {
            Debug.LogError("添加的转换条件为空");
            return;
        }
        if (code.Equals( NpcStateCode.NullState))
        {
            Debug.LogError("添加的转换条件为空");
            return;
        }
        if (!transitionDictionary.ContainsKey(transition))
        {
            transitionDictionary.Add(transition,code);
        }
        else
        {
            Debug.LogError("已经有相同的转换条件了");
        }
    }
    public void RemoveTransition(NpcStateTransition transition, NpcStateCode code)
    {
        if (transition.Equals(NpcStateTransition.NullTransition))
        {
            Debug.LogError("转换条件为空无法移除");
        }
        if (transitionDictionary.ContainsKey(transition))
        {
            transitionDictionary.Remove(transition);
        }
        else
        {
            Debug.LogError("未找到转换条件"+ transition);
        }
    }

    public NpcStateCode GetOutPutNpcState(NpcStateTransition transition)
    {
        if (!transitionDictionary.ContainsKey(transition))
        {
            Debug.Log("没有转换条件"+transition);
            return NpcStateCode.NullState;
            
        }
        else
        {
            return transitionDictionary[transition];
        }
    }

    /// <summary>
    /// 进入该状态之前要做的事情
    /// </summary>
    public virtual void BeforEnterAction() { }
    /// <summary>
    /// 退出该状态之前要做的事情
    /// </summary>
    public virtual void BeforExitAction() { }
    /// <summary>
    /// 是否需要转换到别的状态
    /// </summary>
    /// <param name="targets"></param>
    public abstract void Reason(List<Character> targets);
    /// <summary>
    /// 当前状态所要处理的逻辑
    /// </summary>
    /// <param name="targets"></param>
    public abstract void Act(List<Character> targets);


}

public enum NpcStateCode
{
    NullState=0,
    Idle=1,
    MoveToTarget=2,
    Attack=3,
    Death = 4,
    //Limited=5,
    //Skill=6
}

public enum NpcStateTransition
{
    NullTransition=0,
    /// <summary>
    /// 没有目标，Idle
    /// </summary>
    GetNoTarget,
    /// <summary>
    /// 攻击距离内无敌方，MoveToTarget
    /// </summary>
    FindTargetOutOfAttackRange,
    FindTargetInAttackRange,//攻击距离内有敌方，直接攻击
    NoBlood//血条见底，死亡

}
