using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public  class NpcFMSystem
{
    private List<NpcState> states;
    private NpcStateCode currentStateCode;
    private NpcState currentNpcState;


    public NpcStateCode CurrentStateCode => currentStateCode;
    public NpcState CurrentNpcState => currentNpcState;


    public NpcFMSystem()
    {
        states=new List<NpcState>();
    }

    public void AddNpcState(NpcState state)
    {
        if (states.Exists(p=>p.NpcStateCode.Equals(state.NpcStateCode)))
        {
            Debug.LogError("已经有了这个状态，不要重复添加");
        }
        else
        {
            states.Add(state);
        }
    }

    public void RemoveNpcState(NpcState state)
    {
        if (!states.Exists(p => p.NpcStateCode.Equals(state.NpcStateCode)))
        {
            Debug.LogError("Error!!!没有这个状态！！！");
        }
        else
        {
            states.Remove(state);
        }
    }

    public void ChangeState(NpcStateTransition trans)
    {
        if (trans == NpcStateTransition.NullTransition)
        {
            Debug.LogError("要执行的转换条件为空");
            return;
        }

        NpcStateCode code = currentNpcState.GetOutPutNpcState(trans);
        if (code == NpcStateCode.NullState)
        {
            Debug.LogError("没有转换状态码");
            return;
        }
        else
        {
            currentNpcState.BeforExitAction();

            currentStateCode = code;
            currentNpcState = states.FirstOrDefault(p => p.NpcStateCode.Equals(code));
            CurrentNpcState.BeforEnterAction();
        }
        

    }

    public void Update(List<Character> allCharacters)
    {
        currentNpcState.Reason(allCharacters);
        currentNpcState.Act(allCharacters);
    }

    public void SetInitState(NpcState initState, NpcStateCode initStateCode)
    {
        currentNpcState = initState;
        currentStateCode = initStateCode;
    }
}
