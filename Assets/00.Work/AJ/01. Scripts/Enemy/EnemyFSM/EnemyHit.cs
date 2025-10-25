using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private EnemyAnimator _enemyAnimator;
    public bool isAnimationEnd = false;
    private void Awake()
    {
        _enemyAnimator = GetComponentInChildren<EnemyAnimator>();
    }
    private void Start()
    {
        _enemyAnimator.OnEndTrigger += () => isAnimationEnd = true;
    }
}
