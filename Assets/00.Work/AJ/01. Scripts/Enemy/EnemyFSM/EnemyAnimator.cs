using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    public Action OnAttackStartTrigger;
    public Action OnAttackTrigger;
    public Action OnAttackEndTrigger;
    public Action OnHitEndTrigger;
    public Action OnDeathEndTrigger;
    public Action OnDeathTrigger;
    
    public Action OnAssassinVanish;
    public Action OnAssassinAppearBehind;
    public Action OnAssassinHit;

    public void AttackStart()
    {
        OnAttackTrigger?.Invoke();
    }

    public void Attack()
    {
        OnAttackStartTrigger?.Invoke();
    }
    public void AttackAniamtionEnd()
    {
        OnAttackEndTrigger?.Invoke();
    }

    public void HitAnimationEnd()
    {
        OnHitEndTrigger?.Invoke();
    }
    public void DeathAnimationEnd()
    {
        OnDeathEndTrigger?.Invoke();
    }

    public void Death()
    {
        OnDeathTrigger?.Invoke();
    }
    public void AssashinVanish()
    {
        OnAssassinVanish?.Invoke();
    }

    public void AssashinAppearBehind()
    {
        OnAssassinAppearBehind?.Invoke();
    }
}
