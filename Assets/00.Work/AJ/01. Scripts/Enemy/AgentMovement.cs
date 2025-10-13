using System;
using UnityEngine;

public class AgentMovement : MonoBehaviour, IComponent
{
    [SerializeField] private new Rigidbody2D rigidbody;
    private Agent _owner;
    public Action<Vector2> OnSpeedChange;
    
    private Vector2 _movementInput;

    public void StopImmediately()
    {
        _movementInput = Vector2.zero;
        rigidbody.linearVelocity = Vector2.zero;
    }

    public void SetMovementInput(Vector2 input)
    {
        _movementInput = input.normalized;
    }

    public void FixedUpdate()
    {
        rigidbody.linearVelocity = _movementInput * 3;
        
        OnSpeedChange?.Invoke(rigidbody.linearVelocity);
    }

    public void Initialize(Agent agent)
    {
        _owner = agent;
    }
}
