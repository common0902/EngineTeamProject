using _00.Work.SYH._02Script.ETG;
using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public enum Debuffs
{
    None,
    Slow,
    Bondage,
    Marked
}

public class DebuffController : MonoBehaviour
{
    //public Enemy Enemy { get; private set; }
    public HealthSystem Health { get; private set; }

    float _originSpeed;
    GameObject _markerEffect;
    private void Awake()
    {
        //Enemy = GetComponent<Enemy>();
        Health = GetComponent<HealthSystem>();
        //_originSpeed = Enemy._speed;

        _markerEffect= Resources.Load<GameObject>($"MarkerEffect");
        _markerEffect = Instantiate(_markerEffect, gameObject.transform);
        _markerEffect.SetActive(false);
    }

    public void SetDebuff(Debuffs type, float time, float value)
    {
        switch (type)
        {
            case Debuffs.Slow:
                StartCoroutine(SetSlow(time, value));
                break;
            case Debuffs.Bondage:
                StartCoroutine(SetBondage(time));
                break;
            case Debuffs.Marked:
                SetMarker(value);
                break;
        }
    }
    private IEnumerator SetSlow(float time, float value)
    {
        //Enemy._speed -= value * Enemy._speed;
        yield return new WaitForSeconds(time);
        //Enemy._speed += value * _originSpeed;
    }

    private IEnumerator SetBondage(float time)
    {
        //Enemy._speed = 0;
        yield return new WaitForSeconds(time);
        //Enemy._speed = _originSpeed;
    }

    private void SetMarker(float value)
    {
        _markerEffect.SetActive(true);
        Health._isMarked = true;
        Health._markedValue = value;
    }
    private void OnDisable()
    {
        _markerEffect.SetActive(false);
        Health._isMarked = false;
        Health._markedValue = 1;
        //Enemy._speed = _originSpeed;
        StopAllCoroutines();
    }
}
