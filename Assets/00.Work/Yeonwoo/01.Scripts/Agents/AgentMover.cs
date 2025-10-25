using System;
using _00.Work.Yeonwoo._01.Scripts.Data;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Agents
{
    public class AgentMover : MonoBehaviour, IComponent
    {
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private new Rigidbody2D rb;
        private Agent _owner;
        private Vector2 _movementInput;

        public Action<Vector2> OnSpeedChange;
        
        public void Initialize(Agent agent)
        {
            _owner = agent;
        }

        public void StopImmediately()
        {
            _movementInput = Vector2.zero;
            rb.linearVelocity = Vector2.zero;
        }

        public void SetMovementInput(Vector2 input)
        {
            _movementInput = input.normalized;
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = _movementInput * moveSpeed;
            
            OnSpeedChange?.Invoke(rb.linearVelocity);
        }
    }
}