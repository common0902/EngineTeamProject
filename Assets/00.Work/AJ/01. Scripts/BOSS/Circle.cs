using System;
using _00.Work.SYH._02Script.ETG;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS
{
    public class Circle : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private LayerMask playerMask;
        public bool GetCollision()
        {
            return Physics2D.OverlapCircle(transform.position, radius, playerMask);
        }
        

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
