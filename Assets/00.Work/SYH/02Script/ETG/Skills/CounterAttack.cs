using _00.Work.SYH._02Script.ETG;
using csiimnida.CSILib.SoundManager.RunTime;
using System.Collections;
using UnityEngine;

public class CounterAttack : Skill
{
    GameObject _prefab;
    [SerializeField]bool _counterSucessces;
    protected override void Awake()
    {
        base.Awake();
        _prefab = Instantiate(SkillPrefab, Player.Instance.transform);
        _prefab.SetActive(false);
    }
    protected override void Start()
    {
        base.Start();
        Player.Instance.GetComponent<HealthSystem>().OnCounter += CounterSucesses;
        _prefab.GetComponentInChildren<CounterAttackEvent>().OnEndCounter += () => _counterSucessces = false;
    }
    protected override void Update()
    {
        if (IsActive)
        {
            if (_waitTime >= CoolTime)
            {
                UseSkill();
                _waitTime -= CoolTime;
            }
        }
        _waitTime += Time.deltaTime;
        _waitTime = Mathf.Clamp(_waitTime, 0, CoolTime);
    }
    protected override void UseSkill()
    {
        StartCoroutine(Counter());
        IsActive = false;
    }
    public override void Active()
    {
        base.Active();
        if (_waitTime < CoolTime)
        {
            IsActive = false;
        }
    }
    IEnumerator Counter()
    {
        float waitTime = 0;
        float coolTime = 0.5f;
        Player.Instance.PlayerAnimationCompo.CastingStart();
        Player.Instance.GetComponent<HealthSystem>()._isCounter = true;
        CameraHandler.Instance.Zoomin(4);
        while (waitTime <= coolTime)
        {
            if (_counterSucessces) yield break;
            waitTime += Time.deltaTime;
            yield return null;
        }
        Player.Instance.PlayerAnimationCompo.CastingEnd();
        Player.Instance.GetComponent<HealthSystem>()._isCounter = false;
        CameraHandler.Instance.Zoomout(5);
    }
    void CounterSucesses()
    {
        StopCoroutine(Counter());
        SoundManager.Instance.PlaySound("Parring_Sucesses");
        _counterSucessces = true;
        _prefab.SetActive(true);
    }
}
