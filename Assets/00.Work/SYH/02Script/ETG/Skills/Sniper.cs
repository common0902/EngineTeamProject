using UnityEngine;

public class Sniper : FireBall
{
    float _waitTime2;
    bool _aaa; //지금 사용하는 스킬인가
    protected override void Update()
    {
        base.Update();
        if (_aaa)
        {
            if (_waitTime2 >= 2)
            {
                UseSkill();
            }
            else
            {
                _waitTime2 += Time.deltaTime;
            }
        }
    }
    public override void Passive()
    {
        Player.Instance.PlayerStatusCompo.Speed -= 2;
        _aaa = true;
    }
    public override void DisPassive()
    {
        Player.Instance.PlayerStatusCompo.Speed += 2;
        _waitTime2 = 0;
        _aaa = false;
    }
}
