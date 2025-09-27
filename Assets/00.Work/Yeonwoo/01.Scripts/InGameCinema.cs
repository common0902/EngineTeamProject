using System;
using DG.Tweening;
using UnityEngine;

public class InGameCinema : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSr;

    private void Start()
    {
        _playerSr.DOFade(1, 0.8f);
    }
}
