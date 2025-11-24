using System;
using TMPro;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class InRoomSplashUI : MonoBehaviour
    {
        private TextMeshProUGUI _inRoomText;

        [SerializeField] private Voliere.CleanTitles.DisplayTitle displayTitle; // 인스펙터에 할당 (없으면 Find로 대체)
        private void Awake()
        {
            _inRoomText = GetComponentInChildren<TextMeshProUGUI>();

            if (displayTitle == null)
            {
                displayTitle = FindAnyObjectByType<Voliere.CleanTitles.DisplayTitle>();
                // displayTitle가 null이면 Show 호출 시 안전장치로 로그 출력
                if (displayTitle == null)
                    Debug.LogWarning("DisplayTitle을 찾지 못했습니다. 인스펙터에 할당해 주세요.");
                else
                    displayTitle.gameObject.SetActive(false); // <- 추가: 처음엔 꺼두기
            }
            else
            {
                displayTitle.gameObject.SetActive(false); // 인스펙터에 할당되어 있어도 꺼둠
            }

            if (_inRoomText != null) _inRoomText.text = "";
            // 씬에 이미 존재하는 Room들 구독 (비활성 포함)
            Room[] existingRooms = FindObjectsByType<Room>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
            foreach (Room r in existingRooms)
            {
                SubscribeRoom(r);
            }

            // 이후 런타임에 생성되는 Room들을 받기 위해 static 이벤트 구독
            Room.OnRoomInitialized += SubscribeRoom;
        }

        private void SubscribeRoom(Room room)
        {
            // null 체크 및 중복 구독 방지(간단하게 제거 후 추가)
            room.OnInRoom -= () => HandleOnInRoom(room); // 이 형태로는 람다 제거가 작동하지 않으므로 아래와 같이 로컬 델리게이트 사용
            Action handler = () => HandleOnInRoom(room);
            room.OnInRoom += handler;
            // 참고: 만약 나중에 정확한 제거가 필요하면 Dictionary<Room, Action>으로 관리하세요.
        }

        private void HandleOnInRoom(Room room)
        {
            if (_inRoomText == null) return;

            string title = room.roomType switch
            {
                RoomType.Normal => "전투",
                RoomType.Portal => "포탈",
                RoomType.Shop => "상점",
                RoomType.Gold => "상자",
                RoomType.Boss => "보스",
                _ => ""
            };

            _inRoomText.text = title;

            if (displayTitle != null)
            {
                displayTitle.Show(title);
            }
            else
            {
                Debug.Log($"[InRoomSplashUI] DisplayTitle 없음 — 제목: {title}");
            }
        }

        private void OnDestroy()
        {
            // 정리: static 이벤트 구독 해제
            Room.OnRoomInitialized -= SubscribeRoom;
            // 주의: 개별 room.OnInRoom 구독 제거는 SubscribeRoom에서 handler를 보관해두지 않아서 정확히 제거 못함.
            // 메모리 누수 방지를 위해 필요하면 Dictionary<Room, Action>로 관리 후 여기서 제거하세요.
        }
    }
}
