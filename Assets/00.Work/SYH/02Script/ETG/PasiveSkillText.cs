using TMPro;
using UnityEngine;

public class PasiveSkillText : MonoBehaviour
{
    TextMeshProUGUI _skillText;
    private void Awake()
    {
        _skillText = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        Player.Instance.PasiveSkillControllerCompo.OnTakePasiveSkill += ChangeText;
    }

    private void ChangeText(PasiveSkill skill)
    {
        _skillText.text += ' ' + skill.Name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
