using System;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS
{
    public class LaserBoss : MonoBehaviour
    {
        private Boss _boss;
        public void Init(Boss boss)
        {
            _boss = boss;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                player.PlayerHealthSystemCompo.Damage(10);
                Debug.Log("아아아아아아아아아");
            }
        }

        /*private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                player.PlayerHealthSystemCompo.Damage(_boss.Damage);
                Debug.Log("아아아아아아아아아");
            }
        }*/
    }
}
