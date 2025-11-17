using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public event Action OnChangeSkill;
    public event Action OnChangeUltimateSkill;
    //public Queue<Skill> Skills { get; private set; }
    [field:SerializeField] public List<Skill> Skills { get; private set; }
    [field: SerializeField] public int CurrentAutoAttackNum { get; set; }
    [SerializeField] LayerMask _skillLayer;
    [SerializeField] float _skillChangeRange = 1.5f;
    [SerializeField] AutoAttackListSO AutoAttackList;

    [field:SerializeField] public List<Skill> AutoAttacks { get; private set; }

    public Skill UltimateSkill { get; private set; }
    private void Awake()
    {

        //Skills = new Queue<Skill>();
        //Skills.Enqueue(null);
        //Skills.Enqueue(null);
        //Skills.Enqueue(null);

        Skills = new List<Skill>();
        Skills.Add(null);
        Skills.Add(null);
        AutoAttacks = new List<Skill>();
        foreach (Skill i in AutoAttackList.AutoAttacks)
        {
            AutoAttacks.Add(Instantiate(i.gameObject, transform).GetComponent<Skill>());
        }
    }
    private void Update()
    {
        if (Player.Instance.PlayerMoveCompo._isCasting) return;
        if (Input.GetMouseButtonDown(1))
        {
            UseSkill(true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            UseSkill(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            UseAutoAttack(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            UseAutoAttack(false);
        }
        if(Input.GetMouseButton(1) && Input.GetMouseButton(0))
        {
            UseAutoAttack(false);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, _skillChangeRange, _skillLayer);
            if (collider)
            {
                ChangeSkill(collider.gameObject.GetComponent<Skill>());
            }
        }

        if (Input.mouseScrollDelta != Vector2.zero)
        {
            ScrollSkill(Input.mouseScrollDelta.y);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            UseUltimateSkill();
        }
        
    }

    private void UseAutoAttack(bool onOff)
    {
        if (onOff) AutoAttacks[CurrentAutoAttackNum].Active();
        else AutoAttacks[CurrentAutoAttackNum].DisActive();
    }

    void UseSkill(bool onOff)
    {
        if (UltimateSkill != null && UltimateSkill.Name == "Saber")
        {
            UltimateSkill.GetComponent<Saber>()._skill.Active();
            return;
        }

        if (Skills[0] == null)
        {
            print("스킬 없음");
            return;
        }
        if (!SkillUtility.CanUseSkill(Skills[0].Cost))
        {
            Skills[0].DisActive();
            print("마나 부족"); 
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
    void UseUltimateSkill()
    {
        if (UltimateSkill == null)
        {
            print("궁극기 없음");
            return;
        }
        UltimateSkill.Active();
        print("궁극기 사용");
    }

    public void ChangeSkill(Skill skill)
    {
        if (skill.SkillType==SkillType.UltimateSkill)
        {
            if (UltimateSkill == null)
            {
                UltimateSkill = skill;
                skill.transform.position = new Vector3(9999, 9999, 0);
                skill.Passive();
                return;
            }
            Skill tmp = UltimateSkill;
            UltimateSkill = skill;
            skill.transform.position = new Vector3(9999, 9999, 0);
            tmp.transform.position = transform.position;
            tmp.DisPassive();
            skill.Passive();
            OnChangeUltimateSkill?.Invoke();
            return;
        }

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
        value.transform.position = transform.position;
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

    public void ScrollSkill(float value)
    {
        Skill tmp = Skills[0];
        Skills[0] = Skills[1];
        Skills[1] = tmp;
        OnChangeSkill?.Invoke();
    }
}
