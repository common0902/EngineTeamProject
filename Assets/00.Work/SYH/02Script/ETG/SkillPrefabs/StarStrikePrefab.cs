//using System;
using UnityEngine;

public class StarStrikePrefab : SkillPrefab, IPoolable
{
    public string ItemName => _nameString;
    [SerializeField] string _nameString;

    public GameObject GameObject => gameObject;
    float _tarPos;//낙하할 y좌표
    //private void OnEnable()
    //{
    //    //float rand = (float)Math.Round(UnityEngine.Random.Range(0.75f, 1.5f), 2);
        
    //    //rand=Random.Range(9,12)
    //}
    protected override void Update()
    {
        if (transform.position.y <= _tarPos)
        {
            PoolManager.Instance.Push(GetComponent<IPoolable>());
            gameObject.SetActive(false);
        }
        else
        {
            transform.position -= new Vector3(1, 1, 0) * _shotSpeed * Time.deltaTime;
        }
    }
    public void ResetItem()
    {
        gameObject.SetActive(true);
        float rand = Random.Range(0.75f, 1.5f);
        transform.localScale = new Vector3(rand, rand, 1);
        //_tarPos = Random.Range(-80f, 80f) / 10f; 1안: 랜덤으로 하고 떨어진 곳에 폭발시키기
        _tarPos = -12;//2안:화면 끝까지 떨구기
        transform.position = new Vector3(Random.Range(-5f, 25f), Random.Range(9f,12f), 0);
    }
}
