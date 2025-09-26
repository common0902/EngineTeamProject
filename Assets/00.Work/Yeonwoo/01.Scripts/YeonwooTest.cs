using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class YeonwooTest : MonoBehaviour
{
   [SerializeField] private float _speed = 5f;
   private Rigidbody2D _rb;
   private Vector2 _moveDir;

   private void Awake()
   {
      _rb = GetComponent<Rigidbody2D>();
   }

   private void OnMove(InputValue value)
   {
      _moveDir = value.Get<Vector2>();
   }

   private void FixedUpdate()
   {
      _rb.linearVelocity = _moveDir * _speed;
   }
}
