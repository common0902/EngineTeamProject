using System.Collections.Generic;
using UnityEngine;

public class SkillObsPoolManager : MonoSingleton<SkillObsPoolManager>
{
    [SerializeField] GameObject[] _pool1;
    public Stack<GameObject> SkillPool = new Stack<GameObject>();

    [SerializeField] GameObject[] _pool2;
    public Stack<GameObject> USkillPool = new Stack<GameObject>();
    protected override void Awake()
    {
        base.Awake();
        CreatePool(_pool1);
        CreatePool(_pool2);
        Shuffle();
    }

    private void CreatePool(GameObject[] pool)
    {
        for (int i=0;i<pool.Length;i++)
        {
            GameObject tmp = Instantiate(pool[i], transform);
            pool[i] = tmp;
            tmp.SetActive(false);
        }
    }

    public void Shuffle()
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < _pool1.Length; i++)
        {
            int randNum = rand.Next(_pool1.Length);
            GameObject value = _pool1[randNum];
            _pool1[randNum] = _pool1[i];
            _pool1[i] = value;
        }
        foreach (GameObject i in _pool1)
        {
            SkillPool.Push(i);
        }


        for (int i = 0; i < _pool2.Length; i++)
        {
            int randNum = rand.Next(_pool2.Length);
            GameObject value = _pool2[randNum];
            _pool2[randNum] = _pool2[i];
            _pool2[i] = value;
        }
        foreach (GameObject i in _pool2)
        {
            USkillPool.Push(i);
        }
    }
}
