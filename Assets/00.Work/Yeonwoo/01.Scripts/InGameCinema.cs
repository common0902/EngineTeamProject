using System;
using DG.Tweening;
using UnityEngine;

public class InGameCinema : MonoBehaviour
{
    [SerializeField] private GameObject _test;
    [SerializeField] private float _distance = 3f;
    [SerializeField] private float _duration;
    private Material material;
    
    public event Action ApparanceComplete;

    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        material = spriteRenderer.material;
        _test.transform.position = transform.position;
    }

    private void Start()
    {
        PortalAnim();
    }

    public void PortalAnim()
    {
        Vector3 targetPos = _test.transform.position + Vector3.down * _distance;
        
        _test.transform.DOMove(targetPos, _duration)
            .SetEase(Ease.InOutQuint)
            .OnComplete(() =>
            {
                material.DOFloat(0f, "_SourceGlowDissolveFade", 1f)
                    .SetEase(Ease.OutSine)
                    .OnComplete(() =>
                    {
                        Destroy(gameObject);
                    });
                
                ApparanceComplete?.Invoke();
            });
    }
}
