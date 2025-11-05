using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    public Action OnAttackTrigger;
    public Action OnAttackEndTrigger;
    public Action OnHitEndTrigger;
    public Action OnDeathEndTrigger;

    public void AttackStart()
    {
        OnAttackTrigger?.Invoke();
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
}
