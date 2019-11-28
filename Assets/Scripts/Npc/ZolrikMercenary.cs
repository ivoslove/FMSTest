using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZolrikMercenary : Character
{
   
    public ZolrikMercenary() : base()
    {
        GameObject obj = Resources.Load("ZolrikMercenary Variant") as GameObject;
        GameObject cloneObj = GameObject.Instantiate(obj);
        TransformNode = cloneObj.transform;
        Animator = TransformNode.GetComponent<Animator>();
        Tag = "Red";
        //添加组件
        if (CharacterBehaiverController == null)
            CharacterBehaiverController = cloneObj.AddComponent<ZolrikMercenaryBehavier>();

        //绑定状态机
        FMSystem = new NpcFMSystem();

        //添加状态

        //idle
        NpcIdleState idleState = new NpcIdleState(FMSystem, this);
        idleState.AddTransition(NpcStateTransition.FindTargetInAttackRange, NpcStateCode.Attack);
        idleState.AddTransition(NpcStateTransition.FindTargetOutOfAttackRange, NpcStateCode.MoveToTarget);
        idleState.AddTransition(NpcStateTransition.NoBlood, NpcStateCode.Death);
        FMSystem.AddNpcState(idleState);
        //move
        NpcMoveState moveState = new NpcMoveState(FMSystem, this);
        moveState.AddTransition(NpcStateTransition.GetNoTarget, NpcStateCode.Idle);
        moveState.AddTransition(NpcStateTransition.FindTargetInAttackRange, NpcStateCode.Attack);
        moveState.AddTransition(NpcStateTransition.NoBlood, NpcStateCode.Death);
        FMSystem.AddNpcState(moveState);
        //attack
        NpcAttackState attackState = new NpcAttackState(FMSystem, this);
        attackState.AddTransition(NpcStateTransition.GetNoTarget, NpcStateCode.Idle);
        attackState.AddTransition(NpcStateTransition.FindTargetOutOfAttackRange, NpcStateCode.MoveToTarget);
        attackState.AddTransition(NpcStateTransition.NoBlood, NpcStateCode.Death);
        FMSystem.AddNpcState(attackState);
        //death
        NpcDeathState deathState = new NpcDeathState(FMSystem, this);

        FMSystem.AddNpcState(deathState);
        FMSystem.SetInitState(idleState, NpcStateCode.Idle);
        Debug.Log("雇佣兵初始状态：" + FMSystem.CurrentStateCode);
    }


    public override void EnterDeathState()
    {
        base.EnterDeathState();
        CharacterBehaiverController.DeathBehaiver();
    }

    public override void EnterAttackState()
    {
        base.EnterAttackState();
        CharacterBehaiverController.AttackHahavier();
    }

    public override void UpdateMoveState(Character charactorTargetEnemy)
    {
        base.UpdateMoveState(charactorTargetEnemy);
        CharacterBehaiverController.MovingBehaiver();
    }

    public override void EnterIdleState()
    {
        base.EnterAttackState();
        CharacterBehaiverController.IdleBehaiver();
    }
}
