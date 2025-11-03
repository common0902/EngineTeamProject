using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Action OnAttackTrigger;
    public Action OnAttackEndTrigger;
    public Action OnHitEndTrigger;

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
}
