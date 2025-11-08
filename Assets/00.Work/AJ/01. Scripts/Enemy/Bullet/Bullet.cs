using _00.Work.SYH._02Script.ETG;
using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private EnemySO enemy;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();        
    }

    public void SetUp(Vector2 dir, EnemySO enemySo, float speed)
    {
        enemy = enemySo;
        _rb.linearVelocity = dir * speed;
        StartCoroutine(LifeTimeCoroutine(enemySo.rangedData.bulletData.lifeTime));
    }
    private IEnumerator LifeTimeCoroutine(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out HealthSystem hp))
        {
            hp.Damage(enemy.damage);
            Destroy(gameObject);    
        }
    }
}
