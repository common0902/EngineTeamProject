using UnityEngine;

public class EnemyAttack : MonoBehaviour
{    
    private EnemyAnimator _enemyAnimator;
    private Enemy _enemy;
    private IEnemyAttackBehavior _attackBehavior;
    private float _lastAttackTime = 0f;

    public bool isAnimationEnd
    {
        get => _attackBehavior.IsAttackAnimationEnd;
        set 
        {
            if (_attackBehavior != null)
                _attackBehavior.IsAttackAnimationEnd = value;
        }
    }

    private void Awake()
    {
        _enemyAnimator = GetComponentInChildren<EnemyAnimator>();
        _enemy = GetComponent<Enemy>();

    }

    private void Start()
    {
        InitializeAttackBehavior();
        
        _enemyAnimator.OnAttackTrigger += Attack;
        _enemyAnimator.OnAttackEndTrigger += OnAttackEnd;
        if (_enemy.enemySO.enemyType == EnemyType.Assassin)
        {
            _enemyAnimator.OnAssassinVanish += Vanish;
            _enemyAnimator.OnAssassinAppearBehind += AppearBehind;
        }
    }

    private void InitializeAttackBehavior()
    {
        switch (_enemy.enemySO.enemyType)
        {
            case EnemyType.Melee:
                _attackBehavior = new MeleeAttackBehavior();
                break;
            case EnemyType.Ranged:
                _attackBehavior = new RangedAttackBehavior();
                break;
            case EnemyType.Dash:
                _attackBehavior = new DashAttackBehavior();
                break;
            case EnemyType.Summoner:
                _attackBehavior = new SummonerAttackBehavior();
                break;
            case EnemyType.Assassin:
                _attackBehavior = new AssassinAttackBehavior();
                break;
            case EnemyType.SuisideAttacker:
                _attackBehavior = new SuicideAttackBehavior();
                break;
            case EnemyType.Trapper:
                _attackBehavior = new TrapperAttackBehavior();
                break;
            default:
                Debug.LogError($"Unknown enemy type: {_enemy.enemySO.enemyType}");
                break;
        }

        _attackBehavior.Initialize(_enemy);
    }

    private void OnAttackEnd()
    {
        _attackBehavior.OnAttackAnimationEnd();
    }

    public void Attack()
    {
        _lastAttackTime = Time.time;

        Vector2 dir = (_enemy.Target.position - _enemy.transform.position).normalized;
        
        StartCoroutine(_attackBehavior.ExecuteAttack(dir));
    }

    public void Vanish()
    {
        if (_attackBehavior is AssassinAttackBehavior a)
            a.Vanish();
        
    }

    public void AppearBehind()
    {
        if (_attackBehavior is AssassinAttackBehavior a)
            a.AppearBehind();
    }


    private void OnDestroy()
    {
        _enemyAnimator.OnAttackTrigger -= Attack;
        _enemyAnimator.OnAttackEndTrigger -= OnAttackEnd;
        if (_enemy.enemySO.enemyType == EnemyType.Assassin)
        {
            _enemyAnimator.OnAssassinVanish -= Vanish;
            _enemyAnimator.OnAssassinAppearBehind -= AppearBehind;
        }
    }
}
