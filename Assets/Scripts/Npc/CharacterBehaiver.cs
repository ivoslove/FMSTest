using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class CharacterBehaiver : MonoBehaviour
{

    protected BaseAttack BAttack;

    protected Slider HpSlider;

    protected  Animator AnimatorController;

    public virtual void FinishedOnceWalk()
    {
        Debug.Log("播放了一次walk动画");
    }

    public virtual void FinishedOnceAttack()
    {
        Debug.Log("完成了一次攻击");
    }

    public virtual void StarMoveToTarget(Character targetEnemy)
    {
        Debug.Log("开始向目标移动");
    }

    public virtual void DeathBehaiver()
    {
     
        Debug.Log("死亡");
    }

    public virtual void AttackHahavier()
    {
   
    }

    public virtual void MovingBehaiver()
    {
        
    }

    public virtual void AddAnimatorEvent()
    {

    }

    public virtual void IdleBehaiver()
    {


    }
}
