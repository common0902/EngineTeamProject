using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{
    public bool _cannotMove = true;
    public bool _isCasting;
    bool _isDash;

    private Vector2 _moveDir;
    public event Action<float> OnMoved;
    public event Action OnDisMoved;

    bool _canDash;
    [SerializeField] float _waitTime;
    [SerializeField] float _dashCooltime;
    [SerializeField] float _dashPower;
    [SerializeField] int _dashLayer;
    [SerializeField] int _originLayer;
    TrailRenderer _trail;

    Rigidbody2D _rb;
    BoxCollider2D _collider;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _trail = GetComponentInChildren<TrailRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _trail.time = 0;
        _waitTime = _dashCooltime;
    }

    private void Update()
    {
        if (_waitTime >= _dashCooltime)
        {
            _canDash = true;
        }
        else
        {
            _waitTime += Time.deltaTime;
        }

        _cannotMove = _isCasting | _isDash;
        if (_cannotMove) return;

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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (_canDash)
            {
                StartCoroutine(Dash(aaa));
            }
        }
        OnMove(aaa.normalized);
        if(!_isDash) _rb.linearVelocity = (Vector3)_moveDir * Player.Instance.PlayerStatusCompo.Speed;
    }

    private IEnumerator Dash(Vector2 dir)
    {
        _isDash = true;
        _trail.time = 0.1f;
        gameObject.layer = _dashLayer;
        if (dir == Vector2.zero)
        {
            dir = transform.GetChild(0).localScale.x > 0 ? Vector2.right : Vector2.left;
        }
        _rb.linearVelocity += dir * _dashPower;
        _canDash = false;
        _waitTime = 0;
        yield return null;
        while (Mathf.Abs(_rb.linearVelocityX) + Mathf.Abs(_rb.linearVelocityY) > 2f)
        {
            _rb.linearVelocity *= 0.85f;
            yield return new WaitForSeconds(0.01f);
        }
        _trail.time = 0;
        _isDash = false;
        yield return new WaitForSeconds(0.15f);
        gameObject.layer = _originLayer;
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

