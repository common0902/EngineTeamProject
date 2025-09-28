using System;
using DG.Tweening;
using UnityEngine;

public class ApparancePlayer : MonoBehaviour
{
    [SerializeField] private InGameCinema _portalAnim;
    private Material _material;

    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _material = spriteRenderer.material;
    }

    private void OnEnable()
    {
        _portalAnim.ApparanceComplete += PlayerHologram;
    }

    private void OnDisable()
    {
        _portalAnim.ApparanceComplete -= PlayerHologram;
    }

    private void PlayerHologram()
    {
        _material.DOFloat(0f, "_HologramFade", 2f)
            .SetEase(Ease.Flash);
    }
}
