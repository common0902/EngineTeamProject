using _00.Work.Yeonwoo._01.Scripts.Data;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Agents
{
    public class AgentMover : MonoBehaviour, IComponent
    {
        [SerializeField] private EntityStat _moveSpeed;
        [SerializeField] private Rigidbody2D _rb;
        private Agent _owner;
        private Vector2 _movementInput;
        
        public void Initialize(Agent agent)
        {
            _owner = agent;
        }

        public void StopImmediately()
        {
            
        }
    }
}