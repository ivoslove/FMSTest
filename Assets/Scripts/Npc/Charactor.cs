using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character
{

    #region private numbers

    private string id;

    private string tag;

    private float hp;


    private int attackRange;

    private float baseDamage;
    
    private float def = 5;

    private int level = 1;

    private float moveSpeed = 1f;

    #endregion






    public string Tag
    {
        get => tag;
        set => tag = value;
    }

    public float HP => hp;

    public int AttackRange => attackRange;

    public float BaseDamage => baseDamage;
    /// <summary>
    /// 物理防御
    /// </summary>
  

    public float Def => def;

    /// <summary>
    /// 星级
    /// </summary>


    public int Level => level;

    public float MoveSpeed => moveSpeed;

    public NpcFMSystem FMSystem { get; set; }

    public Transform TransformNode { get; set; }

    public Animator Animator { get; set; }

    public Character TargetEnemy { get; set; }


    public Vector3 CurrrentPosition
    {
        get { return TransformNode.position; }
    }

    public bool IsAlive
    {
        get { return hp > 0; }
    }



    protected CharacterBehaiver CharacterBehaiverController { get; set; }

    #region Ctor
    public Character()
    {
        id = Guid.NewGuid().ToString();
    }

    public Character(int level)
    {
        id = Guid.NewGuid().ToString();
        this.level = level;
    }


    #endregion

    public virtual void MoveTo(Vector3 vector3)
    {
      
    }

    public virtual Character GetTarEnemy(List<Character> allCharacters)
    {
        List<Character> enemys = GetAllEnemys(allCharacters);
        float maxDistance = float.MaxValue;
        int index = -1;
        for (int i = 0; i < enemys.Count; i++)
        {
            var item = enemys[i];
            float distance = Vector3.Distance(item.TransformNode.position, this.TransformNode.position);
            if (distance < maxDistance)
            {
                maxDistance = distance;
                index = i;
            }
        }

        if (index == -1)
        {
            return null;
        }
        TargetEnemy = enemys[index];
        return TargetEnemy;
    }

    public List<Character> GetAllEnemys(List<Character> allCharacters)
    {
        List<Character> enemys = allCharacters.Where(p => !p.Tag.Equals(Tag)&&p.IsAlive).ToList();
        return enemys;
    }



    public void Update(List<Character> allCharacters)
    {
        FMSystem.Update(allCharacters);
    }


    public virtual void EnterIdleState()
    {

    }

    public virtual void EnterDeathState()
    {

    }

    public virtual void EnterAttackState()
    {

    }

    public virtual void UpdateMoveState(Character charactorTargetEnemy)
    {
        
    }

    public virtual void GetBaseAttackDamege(BaseAttack attack)
    {
        this.hp -= (attack.BaseAttackValue -this.def);
    }
}
