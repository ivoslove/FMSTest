using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAttackState :NpcState
{

    public NpcAttackState(NpcFMSystem npcFMS, Character charactor) : base(npcFMS, charactor)
    {
        npcStateCode = NpcStateCode.Attack;
    }

    public override void Reason(List<Character> targets)
    {
        //自己死亡
        if (!charactor.IsAlive)
        {
            fms.ChangeState(NpcStateTransition.NoBlood);
            return;
        }
        //目标死亡
        if (!charactor.TargetEnemy.IsAlive)
        {
            charactor.TargetEnemy = null;
           fms.ChangeState(NpcStateTransition.GetNoTarget);
           return;
        }
        //目标移出攻击距离，追击
        if (Vector3.Distance(charactor.TargetEnemy.CurrrentPosition, charactor.CurrrentPosition) >
            charactor.AttackRange)
        {
            fms.ChangeState(NpcStateTransition.FindTargetOutOfAttackRange);
            return;
        }



    }

    public override void Act(List<Character> targets)
    {
        //先看是不是朝向了敌方，如果没有，先转
        Vector3 dir = charactor.TargetEnemy.CurrrentPosition - charactor.CurrrentPosition;
        Vector3 result = Vector3.Cross(charactor.TransformNode.forward,
            dir);
        //纠正了朝向，在开始攻击
        if (result== Vector3.zero)
        {
            charactor.EnterAttackState();
            return;
        }
        else
        {
            charactor.TransformNode.forward = Vector3.Lerp(charactor.TransformNode.forward, dir,
                GloableValue.RotateLerpSpeed * Time.deltaTime);
            return;
        }

    }
    /// <summary>
    /// 进入攻击状态
    /// </summary>
    public override void BeforEnterAction()
    {
        Debug.Log("准备进入攻击状态");

    }
}
