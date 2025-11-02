using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public event Action OnUseSkill;
    public event Action OnChangeSkill;
    public bool _canUseSkill = true;
    //public Queue<Skill> Skills { get; private set; }
    [field:SerializeField]public List<Skill> Skills { get; private set; }
    [SerializeField] LayerMask _skillLayer;
    [SerializeField] float _skillChangeRange = 1.5f;
    private void Awake()
    {
        //Skills = new Queue<Skill>();
        //Skills.Enqueue(null);
        //Skills.Enqueue(null);
        //Skills.Enqueue(null);

        Skills = new List<Skill>();
        Skills.Add(null);
        Skills.Add(null);
    }
    private void Update()
    {
        if (Player.Instance.PlayerAnimationCompo._isConcentrate) return;
        if (Input.GetMouseButtonDown(0))
        {
            UseSkill(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            UseSkill(false);
        }
        //if (Input.GetMouseButtonDown(1))
        //{
        //    UseSkill(1, true);
        //}
        //else if (Input.GetMouseButtonUp(1))
        //{
        //    UseSkill(1, false);
        //}

        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, _skillChangeRange, _skillLayer);
            if (collider)
            {
                ChangeSkill(collider.gameObject.GetComponent<Skill>());
            }
        }
    }
    public void UseSkill(bool onOff)
    {
        if (!_canUseSkill) return;
        OnUseSkill?.Invoke();
        if (Skills[0] == null)
        {
            print("스킬 없음");
            return;
        }

        if (onOff) Skills[0].Active();
        else Skills[0].DisActive();
            print("스킬 사용");
        #region
        //Skill[] eliments = Skills.ToArray();

        //if (eliments == null || eliments.Length == 0 || eliments[skillNum] == null)
        //{
        //    print("스킬 없음");
        //    return;
        //}

        //if(onOff) eliments[skillNum].Active();
        //else eliments[skillNum].DisActive();
        //---------------------------------------------------------------------------
        #endregion
    }

    public void ChangeSkill(Skill skill)
    {
        for (int i = 0; i < Skills.Count; i++)
        {
            if (Skills[i] == null)
            {
                Skills[i] = skill;
                OnChangeSkill?.Invoke();
                skill.transform.position = new Vector3(9999, 9999, 0);
                return;
            }
        }

        Skill value = Skills[0];
        Skills[0] = skill;
        skill.transform.position = new Vector3(9999, 9999, 0);
        value.gameObject.transform.position = transform.position;
        OnChangeSkill?.Invoke();
        #region
        //Skills.Enqueue(skill);
        //skill.gameObject.SetActive(false);
        //Skill value = Skills.Dequeue();
        //if (value != null)
        //{
        //    //value.gameObject.SetActive(true);
        //    value.gameObject.transform.position = transform.position;
        //}
        #endregion
    }
}
