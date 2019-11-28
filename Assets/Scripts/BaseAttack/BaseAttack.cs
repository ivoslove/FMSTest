using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack
{
    public float BaseAttackValue;

    public BaseAttack(float damageValue)
    {
        BaseAttackValue = damageValue;
    }
    public void DamageNpc(Character npc)
    {
        npc.GetBaseAttackDamege(this);
    }
}
