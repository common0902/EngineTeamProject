using System.Collections;
using UnityEngine;

public interface IEnemyAttackBehavior
{
    void Initialize(Enemy enemy);
    IEnumerator ExecuteAttack(Vector2 direction);
    void OnAttackAnimationEnd();
    bool IsAttackAnimationEnd { get; set; }
}
