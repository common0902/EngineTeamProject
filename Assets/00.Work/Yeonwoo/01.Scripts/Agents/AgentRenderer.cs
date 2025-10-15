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
        }
    }
}