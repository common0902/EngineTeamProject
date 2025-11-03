using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{
    public bool _canMove = true;
    private Vector2 _moveDir;
    public event Action<float> OnMoved;
    public event Action OnDisMoved;

    private void Update()
    {
        if (!_canMove || Player.Instance.PlayerAnimationCompo._isConcentrate) return;
        Vector2 aaa = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            aaa += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            aaa += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            aaa += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            aaa += Vector2.right;
        }
        OnMove(aaa.normalized);
        transform.position += (Vector3)_moveDir * Player.Instance.PlayerStatusCompo.Speed * Time.deltaTime;
    }
    public void OnMove(InputValue value)
    {
        _moveDir = value.Get<Vector2>();
        if (_moveDir == Vector2.zero) OnDisMoved?.Invoke();
        else OnMoved?.Invoke(_moveDir.x);
    }
    public void OnMove(Vector2 value)
    {
        _moveDir = value; 
        if (_moveDir == Vector2.zero) OnDisMoved?.Invoke();
        else OnMoved?.Invoke(_moveDir.x);
    }
}

