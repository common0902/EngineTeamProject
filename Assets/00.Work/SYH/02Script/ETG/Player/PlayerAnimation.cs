using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Sprite _idleImg;
    Animator _ani;
    SpriteRenderer _renderer;
    private void Awake()
    {
        _ani = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        Idle();
    }
    private void Start()
    {
        Player.Instance.PlayerMoveCompo.OnMoved += Move;
        Player.Instance.PlayerMoveCompo.OnDisMoved += Idle;
    }

    public void Idle()
    {
        _renderer.sprite = _idleImg;
        _ani.enabled = false;
    }
    public void Move(float value)
    {
        _ani.enabled = true;
        transform.localScale = value > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1); 
    }
}
