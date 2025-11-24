using System.Collections.Generic;
using UnityEngine;

public class SkillList : MonoBehaviour
{
    [SerializeField] Skill[] _skills;
    public Stack<Skill> SkillPool { get; private set; }
    private void Awake()
    {
        SkillUtility.Shuffle(_skills);
        SkillPool = new Stack<Skill>();
        foreach (Skill i in _skills)
        {
            SkillPool.Push(i);
        }
    }
}