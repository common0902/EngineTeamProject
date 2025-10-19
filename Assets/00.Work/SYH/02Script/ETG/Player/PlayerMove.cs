using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.PokerShooter
{
    public class PlayerMove : MonoBehaviour
    {
        private Vector2 _moveDir;
        private void Update()
        {
            transform.position += (Vector3)_moveDir * Player.Instance.PlayerStatusCompo. _speed * Time.deltaTime;
        }
        public void OnMove(InputValue value)
        {
            _moveDir = value.Get<Vector2>();
        }
    }
}
