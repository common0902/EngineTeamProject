using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public event Action OnUseSkill;
    public event Action OnChangeSkill;
    public bool _canUseSkill = true;
    public Queue<Skill> Skills { get; private set; }
    [SerializeField] LayerMask _skillLayer;
    [SerializeField] float _skillChangeRange = 1.5f;
    private void Awake()
    {
        Skills = new Queue<Skill>();
        Skills.Enqueue(null);
        Skills.Enqueue(null);
        Skills.Enqueue(null);
    }
    //private void Start()
    //{
    //    //InputManager.Instance
    //}
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UseSkill(2);
        }
        if (Input.GetMouseButtonDown(1))
        {
            UseSkill(1);
        }
        if (Input.GetMouseButtonDown(2))
        {
            UseSkill(0);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, _skillChangeRange, _skillLayer);
            if (collider)
            {
                ChangeSkill(collider.gameObject.GetComponent<Skill>());
            }
        }
    }
    public void UseSkill(int skillNum)
    {
        if (!_canUseSkill) return;
        OnUseSkill?.Invoke();
        Skill[] eliments = Skills.ToArray();

        if (eliments == null || eliments.Length == 0 || eliments[skillNum] == null)
        {
            print("스킬 없음");
            return;
        }
        eliments[skillNum].Active();
        print("스킬 사용");
    }

    public void ChangeSkill(Skill skill)
    {
        print(skill);
        Skills.Enqueue(skill);
        skill.gameObject.SetActive(false);
        Skill value = Skills.Dequeue();
        if (value != null)
        {
            value.gameObject.SetActive(true);
            value.gameObject.transform.position = transform.position;
        }
        //print("스킬 변환");
        OnChangeSkill?.Invoke();
    }
}
