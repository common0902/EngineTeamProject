// BattleWinSplashUI.cs (수정본)
using System;
using System.Collections.Generic;
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
        private readonly List<NormalRoom> _subscribedRooms = new List<NormalRoom>();

        private void Awake()
        {
            _winText = GetComponentInChildren<TextMeshProUGUI>();
            if (displayTitle == null)
            {
                displayTitle = FindAnyObjectByType<Voliere.CleanTitles.DisplayTitle>();
            }
        }

        private void OnEnable()
        {
            Debug.Log("[BattleWinSplashUI] OnEnable - 구독 시작");
            Room.OnRoomInitialized += HandleRoomInitialized;

            // 이미 씬에 존재하는 Room들에 대해서도 구독을 보장
            SubscribeToExistingRoomsInScene();
        }

        private void OnDisable()
        {
            Debug.Log("[BattleWinSplashUI] OnDisable - 구독 해제");
            Room.OnRoomInitialized -= HandleRoomInitialized;
            UnsubscribeAllRooms();
        }

        private void HandleRoomInitialized(Room room)
        {
            if (room is NormalRoom normalRoom)
            {
                if (!_subscribedRooms.Contains(normalRoom))
                {
                    Debug.Log($"[BattleWinSplashUI] HandleRoomInitialized - 구독 추가: {normalRoom.name}");
                    normalRoom.OnBattleWin += OnBattleWinHandler;
                    _subscribedRooms.Add(normalRoom);
                }
            }
        }

        private void UnsubscribeAllRooms()
        {
            foreach (var r in _subscribedRooms)
            {
                if (r != null)
                {
                    Debug.Log($"[BattleWinSplashUI] UnsubscribeAllRooms - 해제: {r.name}");
                    r.OnBattleWin -= OnBattleWinHandler;
                }
            }
            _subscribedRooms.Clear();
        }

        private void OnBattleWinHandler(Room room)
        {
            Debug.Log($"[BattleWinSplashUI] OnBattleWinHandler 호출 - 방: {room?.name}");
            // 이 방에 대해서만 해제
            if (room is NormalRoom normalRoom)
            {
                normalRoom.OnBattleWin -= OnBattleWinHandler;
                _subscribedRooms.Remove(normalRoom);
                Debug.Log($"[BattleWinSplashUI] 해당 방 구독 해제 완료: {normalRoom.name}");
            }

            if (_winText != null)
            {
                _winText.text = "승리";
            }
            else
            {
                Debug.LogWarning("[BattleWinSplashUI] _winText가 할당되어 있지 않습니다.");
            }

            if (displayTitle != null)
            {
                displayTitle.Show("승리");
            }
            else
            {
                Debug.LogWarning("[BattleWinSplashUI] DisplayTitle 인스턴스를 찾을 수 없습니다.");
            }
        }

        // 이미 씬에 로드되어 있는 Room 인스턴스들을 찾아 구독 처리
        private void SubscribeToExistingRoomsInScene()
        {
            // 주: Resources.FindObjectsOfTypeAll은 비활성 오브젝트도 찾는다.
            var allRooms = Resources.FindObjectsOfTypeAll<Room>();
            foreach (var room in allRooms)
            {
                // 씬에 실제로 로드된 오브젝트인지 확인 (에셋/프리팹 제외)
                if (room.gameObject.scene.isLoaded)
                {
                    HandleRoomInitialized(room);
                }
            }
        }
    }
}
