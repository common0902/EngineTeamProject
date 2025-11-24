using System.Collections;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Boss2.Attacks
{
    public interface IMBossAttackBehaviour
    {
        void Initialize(MiddleBoss boss);
        IEnumerator ExecuteAttack(Vector2 direction);
        void OnAttackAnimationEnd();
        bool IsAttackAnimationEnd { get; set; }
    }
}