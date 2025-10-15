using System;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float ShotSpeed { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Skill ThisSkill { get; private set; }
    [field: SerializeField] public Sprite SkillSprite { get; private set; }
    [field: SerializeField] public GameObject SkillPrefab { get; private set; }

    public event Action OnUseSkill;
    virtual public void Active()
    {
        OnUseSkill?.Invoke();
    }

    virtual public void Passive()
    {
        
    }
}
