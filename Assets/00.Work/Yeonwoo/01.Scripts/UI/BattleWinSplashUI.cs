using System;
using TMPro;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class BattleWinSplashUI : MonoBehaviour
    {
        private TextMeshProUGUI _winText;

        private void Awake()
        {
            _winText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}