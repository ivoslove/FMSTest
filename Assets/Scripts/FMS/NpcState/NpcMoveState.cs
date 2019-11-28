using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMoveState : NpcState
{
    public NpcMoveState(NpcFMSystem npcFMS, Character charactor) : base(npcFMS, charactor)
    {
        npcStateCode = NpcStateCode.MoveToTarget;
    }

    public override void Reason(List<Character> targets)
    {
        //死亡
        if (!charactor.IsAlive)
        {
            fms.ChangeState(NpcStateTransition.NoBlood);
            return;
        }
        //目标死亡，进入idle状态
        if (!charactor.TargetEnemy.IsAlive)
        {
            charactor.TargetEnemy = null;
            fms.ChangeState(NpcStateTransition.GetNoTarget);
            return;
        }
        //目标距离在攻击距离内,进入攻击状态
        if (Vector3.Distance(charactor.TargetEnemy.CurrrentPosition, charactor.CurrrentPosition) <=
            charactor.AttackRange)
        {
            fms.ChangeState(NpcStateTransition.FindTargetInAttackRange);
            return;
        }

    }

    public override void Act(List<Character> targets)
    {
        //敌方也可能会移动侦听位置，持续导航
        charactor.UpdateMoveState(charactor.TargetEnemy);
        
    }

    public override void BeforEnterAction()
    {
        charactor.UpdateMoveState(charactor.TargetEnemy);
    }
}
