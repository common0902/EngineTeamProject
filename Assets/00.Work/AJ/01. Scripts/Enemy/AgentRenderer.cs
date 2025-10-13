using System;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.Enemy
{
    public class AgentRenderer : MonoBehaviour, IComponent
    {
        public bool IsFacingRight { get; private set; } = true;

        private Agent _owner;
        private SpriteRenderer _spriteRenderer;
        private AgentMovement _mover;

        public void Initialize(Agent agent)
        {
            _owner = agent;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _mover = _owner.GetCompo<AgentMovement>();

            _mover.OnSpeedChange += HandleSpeedChange;
        }

        private void OnDestroy()
        {
            _mover.OnSpeedChange -= HandleSpeedChange;
        }

        private void Filp()
        {
            IsFacingRight = !IsFacingRight;
            float yAngle = IsFacingRight ? 0 : 180f;
            _owner.transform.localEulerAngles = new Vector3(0, yAngle, 0);
        }
        
        private void HandleSpeedChange(Vector2 speed)
        {
            if(IsFacingRight && speed.x < 0 || !IsFacingRight && speed.x > 0)
                Filp();
        }
    }
}