using System;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Agents
{
    public class WayPoint : MonoBehaviour
    {
        public Vector3 Position => transform.position;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.15f);
        }
    }
}