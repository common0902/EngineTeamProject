using System;
using _00.Work.Yeonwoo._01.Scripts.Data;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField]protected float _waitTime;
    [field:SerializeField] public SkillData Data { get; private set; }
    [field: SerializeField] public int Cost { get; protected set; }
    [field: SerializeField] public float PastDelay { get; protected set; }
    [field: SerializeField] public float CoolTime { get; protected set; }
    [field: SerializeField] public string Name { get; protected set; }
    [field: SerializeField] public bool IsActive { get; protected set; }
    [field: SerializeField] public Skill ThisSkill { get; protected set; }
    [field: SerializeField] public Sprite SkillSprite { get; protected set; }
    [field: SerializeField] public GameObject SkillPrefab { get; protected set; }

    public event Action OnUseSkill;
    protected virtual void Awake()
    {
        _waitTime = CoolTime;
    }
    protected virtual void Start()
    {
        Player.Instance.SkillControllerCompo.OnChangeSkill += DisActive;
    }
    virtual public void Active()
    {
        IsActive = true;
    }
    virtual public void DisActive()
    {
        IsActive = false;
    }
    virtual protected void Update()
    {
        if (IsActive)
        {
            if (_waitTime >= CoolTime)
            {
                UseSkill();
                _waitTime -= CoolTime;
            }
        }
        _waitTime += Time.deltaTime * Player.Instance.PlayerStatusCompo._skillCoolDownSpeed / 100;
        _waitTime = Mathf.Clamp(_waitTime, 0, CoolTime);
    }
    virtual protected void UseSkill()
    {
        OnUseSkill?.Invoke();
    }
    virtual public void Passive()
    {
        
    }
    virtual public void DisPassive()
    {

    }

    virtual public void EndPastDelay()
    {

    }
}
