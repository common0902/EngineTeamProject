using System;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Agents
{
    public class AgentRenderer : MonoBehaviour, IComponent
    {
        public bool IsFacingRight { get; private set; } = true;
        
        private SpriteRenderer _sR;
        private Agent _owner;
        private AgentMover _mover;
        
        public void Initialize(Agent agent)
        {
            _owner = agent;
            _sR = GetComponent<SpriteRenderer>();
            _mover = _owner.Get<AgentMover>();

            _mover.OnSpeedChange += HandleSpeedChange;
        }

        private void OnDestroy()
        {
            _mover.OnSpeedChange -= HandleSpeedChange;
        }
        
        private void HandleSpeedChange(Vector2 speed)
        {
            if (IsFacingRight && speed.x < 0 ||
                !IsFacingRight && speed.x > 0)
                Flip();
        }

        private void Flip()
        {
            IsFacingRight = !IsFacingRight;
            float yAngle = IsFacingRight ? 180 : 0;
            
            _owner.transform.localEulerAngles = new Vector3(0, yAngle, 0);
        }
    }
}