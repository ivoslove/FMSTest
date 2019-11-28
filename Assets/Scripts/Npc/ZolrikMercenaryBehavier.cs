using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ZolrikMercenaryBehavier : CharacterBehaiver
{
    // Start is called before the first frame update
    public Animator AnimatorController;

    //public NavMeshAgent NavigationAgent;

    private ZolrikMercenary zolrikMercenary;

    void Start()
    {
        HpSlider = transform.GetComponentInChildren<Slider>();
        AnimatorController = GetComponent<Animator>();
        //NavigationAgent= GetComponent<NavMeshAgent>();
        AddAnimatorEvent();
        zolrikMercenary =
            GameManager.Instance.AllCharacters.FirstOrDefault(p =>
                p is ZolrikMercenary && p.TransformNode.gameObject == this.gameObject) as ZolrikMercenary;
        if (zolrikMercenary == null)
        {
            Debug.LogError("竟然没找到ZolrikMercenary的实例");
            return;
        }
        HpSlider.fillRect.GetComponent<Image>().color = zolrikMercenary.Tag.Equals("Red") ? Color.red : Color.blue;
        //NavigationAgent.stoppingDistance = skeletonKing.AttackRange;


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Camera.main.transform.forward;
        HpSlider.transform.forward = dir;
        HpSlider.value = zolrikMercenary.HP / 100;
    }


    public override void FinishedOnceAttack()
    {

        if (zolrikMercenary.TargetEnemy == null)
        {
            Debug.LogError("目标竟然丢失，攻击失效");
            return;
        }
        else
        {
            Debug.LogError("雇佣兵进行了一次攻击");
            if (BAttack == null)
                BAttack = new BaseAttack(zolrikMercenary.BaseDamage);
            BAttack.DamageNpc(zolrikMercenary.TargetEnemy);
            
        }
    }


    public override void DeathBehaiver()
    {
        base.DeathBehaiver();
        AnimatorController.SetInteger("Idle", 5);
        HpSlider.gameObject.SetActive(false);
    }

    public override void MovingBehaiver()
    {
        base.MovingBehaiver();
        AnimatorController.SetInteger("Idle", 2);


        Vector3 dir = (zolrikMercenary.TargetEnemy.CurrrentPosition - zolrikMercenary.TransformNode.position).normalized;
        zolrikMercenary.TransformNode.forward = Vector3.Lerp(zolrikMercenary.TransformNode.forward,
            dir, GloableValue.RotateLerpSpeed * Time.deltaTime);
        zolrikMercenary.TransformNode.position += dir * zolrikMercenary.MoveSpeed * Time.deltaTime;


      
    }

    public override void AttackHahavier()
    {
        base.AttackHahavier();
        AnimatorController.SetInteger("Idle", 3);
    }

    public override void IdleBehaiver()
    {
        base.IdleBehaiver();
        AnimatorController.SetInteger("Idle", 1);
    }


    public override void AddAnimatorEvent()
    {
        base.AddAnimatorEvent();

        var runtimeAnimatorController = this.AnimatorController.runtimeAnimatorController;
        var attackAnimationClip = runtimeAnimatorController.animationClips.FirstOrDefault(p => p.name.Equals("Attack"));
        attackAnimationClip.AddEvent(new AnimationEvent()
        {
            functionName = "FinishedOnceAttack",
            time = 0.8f
        });
    }
}
