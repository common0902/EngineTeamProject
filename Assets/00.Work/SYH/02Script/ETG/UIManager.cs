using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [field:SerializeField] public TextMeshProUGUI[] SkillUI { get; private set; }
    [field:SerializeField] public SkillController SkillControllerCompo { get; private set; }

    private void Start()
    {
        SkillControllerCompo.OnChangeSkill += ChangeSkillUI;
    }

    private void ChangeSkillUI()
    {
        Skill[] eliments = SkillControllerCompo.Skills.ToArray();
        int j = 0;
        for (int i = 2; i >= 0; i--)
        {
            if (eliments[i] == null)
            {
                SkillUI[i].text = $"Skill{j}: Null";
                j++;
                continue;
            }
            SkillUI[i].text = $"Skill{j}: {eliments[i].Name}";
            j++;
        }
    }
}
