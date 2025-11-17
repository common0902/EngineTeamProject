using System;
using UnityEngine;

public class FireKingAnimationLightning : MonoBehaviour
{
    public Action OnlightningEnd;
    public void LightningEnd()
    {
        OnlightningEnd?.Invoke();
    }
}
