using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{
        private Enemy enemy;
        public Transform player;
        private void Awake()
        {
            enemy = GetComponent<Enemy>();
        }
        private void Start()
        {

        }
        private void Update()
        {
            transform.position += (player.position - transform.position) * enemy.enemySO.speed * Time.deltaTime;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }
}
