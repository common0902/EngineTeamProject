using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "SO/Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Default Setting")]
    public EnemyType enemyType; // 공격 타입(원거리인가 근거리인가)
    public string enemyName; // 에너미 이름
    public Sprite enemySprite; // 에너미 기본 스프라이트
    public float health; // 체력
    public float speed; // 속도
    
    [Header("Patrol Setting")]
    public bool canPatrol = true; // 순찰 할건지
    [Range(0.3f, 0.5f)] public float patrolDist = 0.3f;
    
    [Header("Attack Setting")] // 공격 설정 
    public float damage; // 공격력
    public float attackDelay; // 공격 딜레이

    [Header("Hit Setting")] 
    [Range(0f, 5f)] public float knockbackForce = 3f; // 넉백
    public float knockBackTime = 0.2f; // 넉백 몇초동안 받을건지
    
    [Header("Range")]
    public float chaseRange; // Chase범위
    public float attackRange; // Attack범위
    
    [Header("RangedAttackSetting")] // 원거리 설정
    public GameObject projectilePrefab; // 발사체
    
    [Header("Sound")] 
    public AudioClip attackSound; // 공격 소리(임시)
}
