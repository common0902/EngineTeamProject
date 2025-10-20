using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{
    public bool _canMove = true;
    private Vector2 _moveDir;
    private void Update()
    {
        if(_canMove) transform.position += (Vector3)_moveDir * Player.Instance.PlayerStatusCompo._speed * Time.deltaTime;
    }
    public void OnMove(InputValue value)
    {
        _moveDir = value.Get<Vector2>();
    }
}

