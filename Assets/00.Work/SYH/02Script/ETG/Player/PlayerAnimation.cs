using UnityEngine;
using UnityEngine.Splines;

public class PlayerAnimation : MonoBehaviour
{
    string _dead;
    string _casting;
    Animator _ani;
    SpriteRenderer _renderer;
    readonly int _deadHash = Animator.StringToHash("Dead");
    readonly int _concentrateHash = Animator.StringToHash("Concentrate");
    readonly int _disConcentrateHash = Animator.StringToHash("DisConcentrate");
    readonly int _isMoveHash = Animator.StringToHash("IsMove");

    [SerializeField] PlayerSpriteContainerListSO _spriteContainer;
    int _animatorNum = 0;
    private void Awake()
    {
        _ani = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        SetAnimatorController(0);
        Idle();
    }

    public void SetAnimatorController(int value)
    {
        _animatorNum = value;
        _ani.runtimeAnimatorController = _spriteContainer.Containers[_animatorNum]._animator;
        _casting = _spriteContainer.Containers[_animatorNum]._strings[1];
        _dead = _spriteContainer.Containers[_animatorNum]._strings[0];
    }

    private void Start()
    {
        //_spriteContainer.Containers[0]
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
        _renderer.sprite = _spriteContainer .Containers[_animatorNum].Sprites[_casting];
        _ani.enabled = false;   
    }
    public void CastingEnd()
    {
        _ani.enabled = true;
        _ani.SetTrigger(_disConcentrateHash);
        Player.Instance.PlayerMoveCompo._isCasting = false;
        Idle();
    }
    public void Idle()
    {
        _ani.enabled = true;
        if (!Player.Instance.PlayerMoveCompo._isCasting)
        {
            _ani.SetBool(_isMoveHash, false);
        }
    }
    public void Move(float value)
    {
        _ani.enabled = true;
        _ani.SetBool(_isMoveHash, true);
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
