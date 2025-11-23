using System;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillController : MonoBehaviour
{
    public List<PassiveSkill> PasiveSkills { get; private set; }
    [SerializeField] LayerMask _skillLayer;
    [SerializeField] float _skillChangeRange = 1.5f;
    public event Action<PassiveSkill> OnTakePasiveSkill;
    private void Awake()
    {
        PasiveSkills = new List<PassiveSkill>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, _skillChangeRange, _skillLayer);
            if (collider)
            {
                PassiveSkill skill = collider.gameObject.GetComponent<PassiveSkill>();
                TakeSkill(skill);
                skill.TryBuy();
            }
        }
    }

    private void TakeSkill(PassiveSkill skill)
    {
        PasiveSkills.Add(skill);
        OnTakePasiveSkill?.Invoke(skill);
    }
}
