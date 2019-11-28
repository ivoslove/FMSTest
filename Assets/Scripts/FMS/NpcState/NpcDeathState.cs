using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDeathState : NpcState
{
    public NpcDeathState(NpcFMSystem npcFMS, Character charactor) : base(npcFMS, charactor)
    {
        npcStateCode = NpcStateCode.Death;
    }

    public override void Reason(List<Character> targets)
    {
        
    }

    public override void Act(List<Character> targets)
    {
        
    }

    public override void BeforEnterAction()
    {
        base.BeforEnterAction();
        charactor.EnterDeathState();
    }
}
