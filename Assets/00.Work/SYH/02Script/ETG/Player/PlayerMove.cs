using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{
    public bool _canMove = true;
    private Vector2 _moveDir;
    private void Update()
    {
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
        if (_canMove) transform.position += (Vector3)_moveDir * Player.Instance.PlayerStatusCompo._speed * Time.deltaTime;
    }
    public void OnMove(InputValue value)
    {
        _moveDir = value.Get<Vector2>();
        print(111);
    }
    public void OnMove(Vector2 value)
    {
        _moveDir = value; 
    }
}

