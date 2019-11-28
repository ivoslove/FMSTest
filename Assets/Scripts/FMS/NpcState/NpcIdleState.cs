using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcIdleState : NpcState
{
    public NpcIdleState(NpcFMSystem npcFMS, Character charactor) : base(npcFMS, charactor)
    {
        npcStateCode = NpcStateCode.Idle;
    }

    public override void Reason(List<Character> targets)
    {
        //有敌方目标，就需要跳转状态
        if (!charactor.IsAlive)
        {
            fms.ChangeState(NpcStateTransition.NoBlood);
            return;
        }
        if (charactor.TargetEnemy != null)
        {
            if (charactor.AttackRange >
                Vector3.Distance(charactor.TargetEnemy.CurrrentPosition, charactor.TransformNode.position))
            {
                fms.ChangeState(NpcStateTransition.FindTargetOutOfAttackRange);
            }
            else
            {
                fms.ChangeState(NpcStateTransition.FindTargetInAttackRange);
            }
            return;
        }
    }

    public override void Act(List<Character> targets)
    {
        //idle状态，寻找目标
        if (charactor.TargetEnemy == null)
        {
            var enemyTarget = charactor.GetTarEnemy(targets);
        }
        
    }

    /// <summary>
    /// 进入Idle状态
    /// </summary>
    public override void BeforEnterAction()
    {
        Debug.Log("准备进入Idle状态");
        charactor.EnterIdleState();
    }
}
