using UnityEngine;

public class EnemyAttack : MonoBehaviour
{    
    private EnemyAnimator _enemyAnimator;
    public bool isAnimationEnd = false;
    private Enemy _enemy;
    private void Awake()
    {
        _enemyAnimator = GetComponentInChildren<EnemyAnimator>();
        _enemy = GetComponent<Enemy>();
    }
    private void Start()
    {
        _enemyAnimator.OnAttackTrigger += Attack;
        _enemyAnimator.OnEndTrigger += () => isAnimationEnd = true;
    }
    public void Attack()
    {
        
    }
}
