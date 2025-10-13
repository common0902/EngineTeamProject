using System;
using DG.Tweening;
using UnityEngine;

public class CreditTween : MonoBehaviour
{
    private void Start()
    {
        transform
            .DOLocalRotate(new Vector3(0, 0, 10f), 0.65f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
