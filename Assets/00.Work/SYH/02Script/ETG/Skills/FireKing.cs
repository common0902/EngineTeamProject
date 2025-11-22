using System.Collections;
using UnityEngine;

public class FireKing : Skill
{
    [SerializeField] float _fireKing;
    [SerializeField] float _transformationTime;
    [SerializeField] GameObject _cutScene;
    float _fullHp;
    float _hp;
    SpriteRenderer _black;
    SpriteRenderer _white;
    GameObject _ball;
    GameObject _lightning;

    protected override void Awake()
    {
        base.Awake();
        _cutScene = Instantiate(_cutScene, Player.Instance.transform);
        _cutScene.SetActive(false);

        _black= _cutScene.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _white = _cutScene.transform.GetChild(1).GetComponent<SpriteRenderer>();
        _ball = _cutScene.transform.GetChild(2).gameObject;
        _lightning = _cutScene.transform.GetChild(3).gameObject;
    }
    protected override void Start()
    {
        base.Start();
        _lightning.GetComponent<FireKingAnimationLightning>().OnlightningEnd += AnimationEnd;
    }
    public override void Active()
    {
        base.Active();
        if (_waitTime < CoolTime) IsActive = false;
    }
    protected override void UseSkill()
    {
        StartCoroutine(Transformation());
    }
    IEnumerator Transformation()
    {
        Player.Instance.PlayerAnimationCompo.CastingStart();
        Player.Instance.Mujuck(true);
        _cutScene.SetActive(true);
        StartCoroutine(IntroManager.Instance.Show(1, _black));
        yield return new WaitForSeconds(4);

        _black.color = new Color(0, 0, 0, 0);
        _white.color = new Color(1, 1, 1, 1);
        _ball.SetActive(false);
        _lightning.SetActive(true);
        StartCoroutine(IntroManager.Instance.Hide(0.5f, _white));
        yield return new WaitForSeconds(0.5f);

        IsActive = false;
        Player.Instance.PlayerAnimationCompo.SetAnimatorController(1);
        Player.Instance.PlayerStatusCompo._fireKing = _fireKing;
        _fullHp = Player.Instance.PlayerStatusCompo._fullHp;
        Player.Instance.PlayerStatusCompo._fullMana += _fullHp;
        _hp = Player.Instance.PlayerStatusCompo._hp;
        Player.Instance.PlayerStatusCompo.Mana += _hp;
        Player.Instance.PlayerStatusCompo._skillCoolDownSpeed += 100;
        Player.Instance.SkillControllerCompo.CurrentAutoAttackNum = 2;
        Player.Instance.PlayerStatusCompo._fullHp = 0;
        Player.Instance.PlayerStatusCompo._hp = 0;
        Player.Instance._isFireKing = true;

        Player.Instance.PlayerAnimationCompo.CastingEnd();
        StartCoroutine(TransformationTime());
    }
    IEnumerator TransformationTime()
    {
        yield return new WaitForSeconds(_transformationTime - 4);
        Player.Instance.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(4);
        Player.Instance.PlayerAnimationCompo.SetAnimatorController(0);
        IntroManager.Instance._white.color = new Color(1, 1, 1, 1);
        StartCoroutine(IntroManager.Instance.Hide(0.75f, IntroManager.Instance._white));
        yield return new WaitForSeconds(0.75f);
        Player.Instance.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1);


        Player.Instance.Mujuck(false);
        Player.Instance.PlayerStatusCompo._fireKing = 1;
        if (Player.Instance.PlayerStatusCompo.Mana < _hp)
        {
            Player.Instance.PlayerStatusCompo._hp += Player.Instance.PlayerStatusCompo.Mana;
            Player.Instance.PlayerStatusCompo.Mana = 0;
        }
        else
        {
            Player.Instance.PlayerStatusCompo.Mana -= _hp;
            Player.Instance.PlayerStatusCompo._hp = _hp;
        }
        Player.Instance.PlayerStatusCompo._fullMana -= _fullHp;
        Player.Instance.PlayerStatusCompo._fullHp = _fullHp;
        Player.Instance.PlayerStatusCompo._skillCoolDownSpeed -= 100;

        Player.Instance.SkillControllerCompo.CurrentAutoAttackNum = 0;

        
        Player.Instance._isFireKing = false;
    }
    public void AnimationEnd()
    {
        _ball.SetActive(true);
        _lightning.SetActive(false);
        _cutScene.SetActive(false);
    }
}
