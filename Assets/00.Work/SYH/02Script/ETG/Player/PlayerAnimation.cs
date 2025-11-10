using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Sprite _idleImg;
    [SerializeField] Sprite _deadImg;
    [SerializeField] Sprite _concentrateImg;
    Animator _ani;
    SpriteRenderer _renderer;
    readonly int _deadHash = Animator.StringToHash("Dead");
    readonly int _concentrateHash = Animator.StringToHash("Concentrate");
    readonly int _disConcentrateHash = Animator.StringToHash("DisConcentrate");
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            CastingStart();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            CastingEnd();
        }
    }
    public void CastingStart()
    {
        _ani.enabled = true;
        _ani.SetTrigger(_concentrateHash);
        Player.Instance.PlayerMoveCompo._isCasting = true;
    }
    public void Casting()
    {
        _renderer.sprite = _concentrateImg;
        _ani.enabled = false;
    }
    public void CastingEnd()
    {
        _ani.enabled = true;
        _ani.SetTrigger(_disConcentrateHash);
        Idle();
        Player.Instance.PlayerMoveCompo._isCasting = false;
    }
    public void Idle()
    {
        if (!Player.Instance.PlayerMoveCompo._isCasting)
        {
            _renderer.sprite = _idleImg;
            _ani.enabled = false;
        }
    }
    public void Move(float value)
    {
        _ani.enabled = true;
        //transform.localScale = value > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1); 
        if (value > 0) transform.localScale = new Vector3(1, 1, 1);
        else if( value < 0) transform.localScale = new Vector3(-1, 1, 1);
    }
    public void Dead()
    {
        _ani.enabled = true;
        _ani.SetTrigger(_deadHash);
    }
    public void CounterAttack()
    {

    }
}
