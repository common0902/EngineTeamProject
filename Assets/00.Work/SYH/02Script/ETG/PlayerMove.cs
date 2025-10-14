using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.PokerShooter
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] float _speed = 5;
        private Vector2 _moveDir;
        private void Update()
        {
            transform.position += (Vector3)_moveDir * _speed * Time.deltaTime;
        }
        public void OnMove(InputValue value)
        {
            _moveDir = value.Get<Vector2>();
        }
    }
}
