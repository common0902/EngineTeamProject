// BattleWinSplashUI.cs
using System;
using TMPro;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class BattleWinSplashUI : MonoBehaviour
    {
        [Tooltip("인스펙터에서 할당하세요. 없으면 런타임에 찾아 사용합니다.")]
        [SerializeField] private Voliere.CleanTitles.DisplayTitle displayTitle;

        private TextMeshProUGUI _winText;

        // 구독한 NormalRoom 인스턴스(나중에 해제하기 위함)
        private readonly System.Collections.Generic.List<NormalRoom> _subscribedRooms = new System.Collections.Generic.List<NormalRoom>();

        private void Awake()
        {
            _winText = GetComponentInChildren<TextMeshProUGUI>();
            // displayTitle가 인스펙터에 없으면 런타임에 찾아본다 (없을 수도 있음)
            if (displayTitle == null)
            {
                displayTitle = FindAnyObjectByType<Voliere.CleanTitles.DisplayTitle>();
            }
        }

        private void OnEnable()
        {
            // Room이 초기화될 때마다 확인해서 NormalRoom이면 구독
            Room.OnRoomInitialized += HandleRoomInitialized;
        }

        private void OnDisable()
        {
            Room.OnRoomInitialized -= HandleRoomInitialized;
            UnsubscribeAllRooms();
        }

        private void HandleRoomInitialized(Room room)
        {
            if (room is NormalRoom normalRoom)
            {
                // 중복 구독 방지
                if (!_subscribedRooms.Contains(normalRoom))
                {
                    normalRoom.OnBattleWin += OnBattleWinHandler;
                    _subscribedRooms.Add(normalRoom);
                }
            }
        }

        private void UnsubscribeAllRooms()
        {
            foreach (var r in _subscribedRooms)
            {
                if (r != null) r.OnBattleWin -= OnBattleWinHandler;
            }
            _subscribedRooms.Clear();
        }

        private void OnBattleWinHandler()
        {
            // 텍스트 변경
            if (_winText != null)
            {
                _winText.text = "승리";
            }
            else
            {
                Debug.LogWarning("[BattleWinSplashUI] _winText가 할당되어 있지 않습니다.");
            }

            // DisplayTitle 실행(있으면 실행)
            if (displayTitle != null)
            {
                displayTitle.Show("승리");
            }
            else
            {
                Debug.LogWarning("[BattleWinSplashUI] DisplayTitle 인스턴스를 찾을 수 없습니다.");
            }
        }
    }
}
