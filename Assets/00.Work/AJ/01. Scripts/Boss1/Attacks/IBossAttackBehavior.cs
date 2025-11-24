using System.Collections;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS.Attacks
{
    public interface IBossAttackBehavior
    {
        void Initialize(Boss boss);
        IEnumerator ExecuteAttack(Vector2 direction);
        void OnAttackAnimationEnd();
        bool IsAttackAnimationEnd { get; set; }
    }
}