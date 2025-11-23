using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.Yeonwoo._01.Scripts.Direction
{
    public class InPortalManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _interactionText;
        private readonly float _range = 2f;
        private Transform _playerPos;
        
        public event Action PlayerPortalIn;
        private void OnEnable()
        {
            if (_interactionText) 
                _interactionText.gameObject.SetActive(false);
            GameObject player = Player.Instance.gameObject;
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
            float distance = Vector3.Distance(transform.position, _playerPos.position);

            if (distance <= _range)
            {
                _interactionText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    PlayerPortalIn?.Invoke();
                    print(222);
                    NextStage();
                }
            }
            else
            {
                _interactionText.gameObject.SetActive(false);
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
