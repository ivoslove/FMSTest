using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SkeletonKingBehavier : CharacterBehaiver
{
    // Start is called before the first frame update


    //public NavMeshAgent NavigationAgent;

    private SkeletonKing skeletonKing;

    //public Texture2D HPBGColorTextrue;

    //public Texture2D HPLeftTextrue;


    void Start()
    {
        HpSlider = transform.GetComponentInChildren<Slider>();

          AnimatorController = GetComponent<Animator>();
        //NavigationAgent= GetComponent<NavMeshAgent>();
        AddAnimatorEvent();
        skeletonKing =
            GameManager.Instance.AllCharacters.FirstOrDefault(p =>
                p is SkeletonKing && p.TransformNode.gameObject == this.gameObject) as SkeletonKing;
        if (skeletonKing == null)
        {
            Debug.LogError("竟然没找到SkeletonKing的实例");
            return;
        }

        HpSlider.fillRect.GetComponent<Image>().color = skeletonKing.Tag.Equals("Red") ? Color.red : Color.blue;
        //NavigationAgent.stoppingDistance = skeletonKing.AttackRange;


    }

    // Update is called once per frame
    void Update()
    {
        if ( HpSlider.gameObject.activeSelf)
        {
            Vector3 dir = Camera.main.transform.forward;
            HpSlider.transform.forward = dir;
            HpSlider.value = skeletonKing.HP / 100;
        }

    }


    public override void FinishedOnceAttack()
    {

        if (skeletonKing.TargetEnemy == null)
        {
            Debug.LogError("目标竟然丢失，攻击失效");
            return;
        }
        else
        {
            Debug.LogError("骷髅王进行了一次攻击");
            if (BAttack == null)
                BAttack = new BaseAttack(skeletonKing.BaseDamage);
            BAttack.DamageNpc(skeletonKing.TargetEnemy);
        }
    }


    public override void DeathBehaiver()
    {
        base.DeathBehaiver();
        AnimatorController.SetInteger("Idle",7);
        HpSlider.gameObject.SetActive(false);
    }

    public override void MovingBehaiver()
    {
        base.MovingBehaiver();
        AnimatorController.SetInteger("Idle",5);


        Vector3 dir = (skeletonKing.TargetEnemy.CurrrentPosition - skeletonKing.TransformNode.position).normalized;
        skeletonKing.TransformNode.forward= Vector3.Lerp(skeletonKing.TransformNode.forward,
            dir, GloableValue.RotateLerpSpeed * Time.deltaTime);
        skeletonKing.TransformNode.position += dir * skeletonKing.MoveSpeed * Time.deltaTime;
       

        //NavigationAgent.SetDestination(skeletonKing.TargetEnemy.CurrrentPosition);
        //Debug.Log("当前目标点坐标："+ skeletonKing.TargetEnemy.CurrrentPosition);
    }

    public override void AttackHahavier()
    {
        base.AttackHahavier();
        AnimatorController.SetInteger("Idle", 4);
    }

    public override void AddAnimatorEvent()
    {
        base.AddAnimatorEvent();

        var runtimeAnimatorController = this.AnimatorController.runtimeAnimatorController;
        var attackAnimationClip =  runtimeAnimatorController.animationClips.FirstOrDefault(p=>p.name.Equals("Attack"));
        attackAnimationClip.AddEvent(new AnimationEvent()
        {
            functionName = "FinishedOnceAttack",
            time = 0.8f
        });
    }

    public override void IdleBehaiver()
    {
        base.IdleBehaiver();
        AnimatorController.SetInteger("Idle", 1);
    }

    //void OnGUI()
    //{
    //    //得到NPC头顶在3D世界中的坐标
    //    //默认NPC坐标点在脚底下，所以这里加上npcHeight它模型的高度即可
    //    Vector3 worldPosition = new Vector3(transform.position.x, transform.position.y + 1.8f, transform.position.z);
    //    //根据NPC头顶的3D坐标换算成它在2D屏幕中的坐标
    //    Vector2 position = Camera.main.WorldToScreenPoint(worldPosition);
    //    //得到真实NPC头顶的2D坐标
    //    position = new Vector2(position.x, Screen.height - position.y);
    //    //注解2
    //    //计算出血条的宽高
    //    Vector2 bloodSize = GUI.skin.label.CalcSize(new GUIContent(HPBGColorTextrue));

    //    //通过血值计算红色血条显示区域
    //    int blood_width = HPLeftTextrue.width * (int)skeletonKing.HP/ 100;
    //    //先绘制黑色血条
    //    GUI.DrawTexture(new Rect(position.x - (bloodSize.x / 2), position.y - bloodSize.y, bloodSize.x, bloodSize.y), HPBGColorTextrue);
    //    //在绘制红色血条
    //    GUI.DrawTexture(new Rect(position.x - (bloodSize.x / 2), position.y - bloodSize.y, blood_width, bloodSize.y), HPLeftTextrue);

    //    ////注解3
    //    ////计算NPC名称的宽高
    //    //Vector2 nameSize = GUI.skin.label.CalcSize(new GUIContent(name));
    //    ////设置显示颜色为黄色
    //    //GUI.color = Color.yellow;
    //    ////绘制NPC名称
    //    //GUI.Label(new Rect(position.x - (nameSize.x / 2), position.y - nameSize.y - bloodSize.y, nameSize.x, nameSize.y), name);

    //}
}
