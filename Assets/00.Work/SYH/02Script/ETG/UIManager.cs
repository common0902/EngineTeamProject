using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [field:SerializeField] public TextMeshProUGUI[] SkillUI { get; private set; }
    [field:SerializeField] public SkillController SkillControllerCompo { get; private set; }
    [field:SerializeField]public PassiveSkillText PassiveSkillTextCompo { get; private set; }

    private void Start()
    {
        SkillControllerCompo.OnChangeSkill += ChangeSkillUI;
    }

    private void ChangeSkillUI()
    {
        for (int i = 0; i < Player.Instance.SkillControllerCompo.Skills.Count; i++)
        {
            if (Player.Instance.SkillControllerCompo.Skills[i] == null)
            {
                SkillUI[i].text = $"Skill{i}: Null";
                continue;
            }
            SkillUI[i].text = $"Skill{i}: {Player.Instance.SkillControllerCompo.Skills[i].Name}";
        }
    }
}
