using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.Yeonwoo._01.Scripts.Direction
{
    public class InPortalManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI interactionText;
        private readonly float _range = 2f;
        private Transform _playerPos;
        
        public event Action<Action> PlayerPortalIn;
        
        private void OnEnable()
        {
            if (interactionText) 
                interactionText.gameObject.SetActive(false);
            GameObject player = GameObject.Find("Player"); // 에디터 이름으로 찾는거라 이름은 Player로 계속 유지
            if (player != null)
            {
                _playerPos = player.transform;
            }
        }

        private void Update()
        {
            Interaction();
        }

        private void Interaction()
        {
            if (_playerPos == null) return;
            
            float distance = Vector3.Distance(transform.position, _playerPos.position);

            if (distance <= _range)
            {
                interactionText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                        NextStage();
                }
            }
            else
            {
                interactionText.gameObject.SetActive(false);
            }
        }

        private void NextStage()
        {
            StartCoroutine(IntroManager.Instance.Show(0.01f, IntroManager.Instance._black));
            _playerPos.position = Vector3.zero;
            RoomManager.Instance.RegenerateRooms();
        }
    }
}
