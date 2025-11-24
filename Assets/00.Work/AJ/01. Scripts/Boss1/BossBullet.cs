using _00.Work.AJ._01._Scripts.BOSS.FSM.States;
using _00.Work.SYH._02Script.ETG;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS
{
    public class BossBullet : MonoBehaviour
    {
        public float Damage { get; set; }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                if (player.TryGetComponent(out HealthSystem healthSystem))
                {
                    healthSystem.Damage(Damage);
                }
            }
            Destroy(gameObject);
        }
    }
}