using System.Collections;
using UnityEngine;

public class StarStrike : Skill 
{
    GameObject _prefab;
    [SerializeField] GameObject _bigStar;
    protected override void Awake()
    {
        base.Awake();
        _prefab = Instantiate(SkillPrefab, Vector3.zero, Quaternion.identity);
        _prefab.SetActive(false);
    }
    protected override void UseSkill()
    {
        base.UseSkill();
        _prefab.SetActive(true);
        CameraHandler.Instance.ShakeCamera(0.03f, 20);
        Player.Instance.PlayerAnimationCompo.CastingStart();
        CameraHandler.Instance.MovedTarget(_prefab.transform);
        StartCoroutine(SummonStar());
    }
    private IEnumerator SummonStar()
    {
        float waitTIme = 0;
        float revolution = 0.5f;
        while (waitTIme < 18)
        {
            PoolManager.Instance.Pop(Name);
            waitTIme += revolution + Time.deltaTime;
            yield return new WaitForSeconds(revolution);
            revolution -= revolution / 20f;
            revolution = Mathf.Clamp(revolution, 0.2f, 0.5f);
        }
        CameraHandler.Instance.ShakeCamera(0.045f, 17);
        PoolManager.Instance.Pop("BigStar");
        yield return new WaitForSeconds(2);
        StartCoroutine(IntroManager.Instance.Show(8));
        yield return new WaitForSeconds(8);
        CameraHandler.Instance.MovedTarget(Player.Instance.transform);
        yield return new WaitForSeconds(3);
        StartCoroutine(IntroManager.Instance.Hide(2.5f));
        _prefab.SetActive(false);
        yield return new WaitForSeconds(2.5F);
        Player.Instance.PlayerAnimationCompo.CastingEnd();
    }
}
