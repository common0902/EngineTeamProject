using System;
using System.Collections.Generic;
using UnityEngine;

public class PasiveSkillController : MonoBehaviour
{
    public List<PasiveSkill> PasiveSkills { get; private set; }
    [SerializeField] LayerMask _skillLayer;
    [SerializeField] float _skillChangeRange = 1.5f;
    public event Action<PasiveSkill> OnTakePasiveSkill;
    private void Awake()
    {
        PasiveSkills = new List<PasiveSkill>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, _skillChangeRange, _skillLayer);
            if (collider)
            {
                PasiveSkill skill = collider.gameObject.GetComponent<PasiveSkill>();
                TakeSkill(skill);
                skill.Take();
            }
        }
    }

    private void TakeSkill(PasiveSkill skill)
    {
        PasiveSkills.Add(skill);
        OnTakePasiveSkill?.Invoke(skill);
    }
}
