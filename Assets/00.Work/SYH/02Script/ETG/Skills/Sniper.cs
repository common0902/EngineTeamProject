using UnityEngine;

public class Sniper : FireBall
{
    float _waitTime2;
    bool _isFirstSkill; //지금 사용하는 스킬인가
    bool _isStoped;
    [SerializeField] float _needWaitTime2;
    [SerializeField] float _sniper;//스나이퍼 상태일 때 대미지 계수
    protected override void Start()
    {
        base.Start();
        Player.Instance.PlayerMoveCompo.OnMoved += (float a) => _isStoped = false;
        Player.Instance.PlayerMoveCompo.OnDisMoved += () => _isStoped = true;
        //Player.Instance.SkillControllerCompo.OnChangeSkill += FirstSkillOption;
    }
    protected override void Update()
    {
        base.Update();
        if (_isFirstSkill && _isStoped)
        {
            if (_waitTime2 >= _needWaitTime2)
            {
                Player.Instance.PlayerStatusCompo._sniper = _sniper;
            }
            else
            {
                _waitTime2 += Time.deltaTime;
            }
        }
        else
        {
            Player.Instance.PlayerStatusCompo._sniper = 1;
            _waitTime2 = 0;
        }
    }
    void FirstSkillOption()
    {
        if (Player.Instance.SkillControllerCompo.Skills[0] == this)
        {
            Passive();
        }
        else
        {
            DisPassive();
        }
    }
    public override void Passive()
    {
        if (_isFirstSkill) return;
        Player.Instance.PlayerStatusCompo._speed -= 1.5f;
        _isFirstSkill = true;
    }
    public override void DisPassive()
    {
        if (!_isFirstSkill) return;
        Player.Instance.PlayerStatusCompo._speed += 1.5f;
        _waitTime2 = 0;
        Player.Instance.PlayerStatusCompo._sniper = 1;
        _isFirstSkill = false;
    }
}
