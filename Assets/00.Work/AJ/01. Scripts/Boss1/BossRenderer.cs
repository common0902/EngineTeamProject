using System;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS
{
    public class BossRenderer : MonoBehaviour
    {
        private Boss _owner;

        private void Awake()
        {
            _owner = GetComponentInParent<Boss>();
        }

        public void Flip(Vector2 value)
        {
            Vector3 currentRotation = _owner.transform.eulerAngles;
        
            if(value.x > 0)
                _owner.transform.rotation = Quaternion.Euler(currentRotation.x, 0, 0);
            else
                _owner.transform.rotation = Quaternion.Euler(currentRotation.x, 180, 0);
        }
    }
}