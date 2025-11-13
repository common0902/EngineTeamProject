using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Enemy", menuName = "SO/Enemy/EnemyData")]
public class EnemySO : ScriptableObject
{
    [Header("Default Setting")]
    public EnemyType enemyType; // 공격 타입(원거리인가 근거리인가)
    public string enemyName; // 에너미 이름
    public Sprite enemySprite; // 에너미 기본 스프라이트
    public float speed; // 속도
    public float health;
    
    [Header("Patrol Setting")]
    public bool canPatrol = true; // 순찰 할건지
    [Range(0.3f, 0.5f)] public float patrolDist = 0.3f;
    
    [Header("Attack Setting")] // 공격 설정 
    public float damage; // 공격력
    public float attackDelay; // 공격 딜레이 - 

    public bool useBoxRange;
    public Vector2 boxRange;

    [Header("Hit Setting")] 
    [Range(0f, 10f)] public float knockbackForce = 0.1f; // 넉백
    public float knockBackTime = 0.2f; // 넉백 몇초동안 받을건지
    
    [Header("Range")]
    public float chaseRange; // Chase범위
    public float attackRange; // Attack범위
    public float deathRange; // 죽는 범위 (폭발하는 적만)

    public float deathDamage; // 죽었을때 얼마만큼의 피해를 줄건지
    
    [Header("Types Setting")]
    public EnemyRangedData rangedData;
    public EnemyDashData dashData;
    public EnemySummonerData summonerData;
    public EnemyAssassinData assassinData;
    public EnemySuicideAttackerData suicideAttackerData;
    public EnemyTrapperData trapperData;
    
    [Header("Sound")] 
    public AudioClip attackSound; // 공격 소리(임시)

    private void OnValidate()
    {
        chaseRange = Mathf.Max(chaseRange, attackRange);
    }
}

[Serializable]
public class EnemyRangedData
{
    [Header("RangedAttackSetting")] // 원거리 설정
    public BulletData bulletData; // 불렛 데이터
}

[Serializable]
public class EnemyDashData
{
    [Header("DashAttackSetting")] // 데쉬 설정
    public float dashForce = 5f; // 데시 할때 얼마만큼의 힘으로 쏠건지
    public float dashAttackTime = 0.5f; // 데쉬 공격 지속 시간
}

[Serializable]
public class EnemySummonerData
{
    [Header("SummonerSetting")]
    public GameObject[] summonPrefab; // 스폰할 객체
    public int maxSummonCount = 3; // 몇마리 스폰할건지
    public float summonRange = 3f; // 스폰 범위
    public float waitforNextSummon = 10f; // 꽉차면 다음 스폰까지 몇초나 기다릴건지
    public bool isLifeTimeInChildren = false; // Clone이 죽는 시간이 있을것인지
    public float summonLifeTime = 10f; // Clone이 죽는 시간
    public bool isFade = true; // Fade등장할건지
}

[Serializable]
public class EnemyAssassinData
{
    [Header("AssassinSetting")] 
    public ParticleSystem vanishVfx;
    public float hideDuration = 0.6f;      // 사라져 있는 시간
    public float appearBehindDistance = 0.8f; // 플레이어 뒤 얼마나 떨어져서 나타날지
    public bool randomHideDuration = false;
    public float minHideDuration = 0.4f; 
}

[Serializable]
public class EnemySuicideAttackerData
{
    [Header("SuicideAttackerSetting")] 
    public ParticleSystem particleSystem;
    public float explosionRadius = 1.5f;
    public float explosionDelay = 0.3f;
}

[Serializable]
public class EnemyTrapperData
{
    [Header("Trap Settings")]
    public GameObject trapPrefab;  // 함정 프리팹
    [Range(1, 10)] public int maxTraps = 3; // 최대 함정 개수
    public float trapPlaceInterval = 2f; // 함정 설치 간격

    [Header("Trap Behavior")]
    public float trapLifetime = 15f; // 함정 지속 시간
    public float trapActivationDelay = 0.5f; // 활성화 딜레이
    public float trapTriggerRadius = 0.8f; // 감지 반경
    public float trapDamage = 10f; // 함정 데미지
    
    public ParticleSystem placeTrapVFX; // 설치 이펙트
    public Color trapWarningColor = Color.red; // 경고 색상
}