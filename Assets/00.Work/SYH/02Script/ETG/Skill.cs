using System;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField]float _waitTime;
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public float PastDelay { get; private set; }
    [field: SerializeField] public float CoolTime { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public bool IsActive { get; private set; }
    [field: SerializeField] public Skill ThisSkill { get; private set; }
    [field: SerializeField] public Sprite SkillSprite { get; private set; }
    [field: SerializeField] public GameObject SkillPrefab { get; private set; }

    public event Action OnUseSkill;
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

    virtual public void EndPastDelay()
    {

    }
}
