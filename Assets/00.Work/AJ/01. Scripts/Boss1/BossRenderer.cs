using System;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS
{
    public class BossRenderer : MonoBehaviour
    {
        private Transform _target;
        
        public void Init(Transform target)
        {
            _target = target;
        }
        
        public void Init(MonoBehaviour target)
        {
            _target = target.transform;
        }
        
        public void Init(GameObject target)
        {
            _target = target.transform;
        }
        
        public void Flip(Vector2 value)
        {
            if (_target == null) return;
            
            Vector3 currentRotation = _target.transform.eulerAngles;
        
            if(value.x > 0)
                _target.transform.rotation = Quaternion.Euler(currentRotation.x, 0, 0);
            else
                _target.transform.rotation = Quaternion.Euler(currentRotation.x, 180, 0);
        }
    }
}