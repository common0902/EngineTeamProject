using System;
using TMPro;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class InteractionBox : MonoBehaviour
    {
        private readonly float _interactionRange = 1.25f;
        private Transform _playerPos;
        public TextMeshProUGUI InteractionText { get; private set; }

        public event Action OnBoxOpen;

        private void Awake()
        {
            InteractionText = GetComponentInChildren<TextMeshProUGUI>();
            if (InteractionText == null)
                Debug.LogError("InteractionBox: No InteractionText attached!");
            GameObject player = GameObject.Find("Player");
            if (player != null)
                _playerPos = player.transform;
        }

        private void Start()
        {
            InteractionText.gameObject.SetActive(false);
        }

        private void Update()
        {
            BoxOpen();
        }

        private void BoxOpen()
        {
            float distance = Vector3.Distance(transform.position, _playerPos.position);

            if (distance <= _interactionRange)
            {
                InteractionText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    OnBoxOpen?.Invoke();
                    this.enabled = false;
                }
            }
            else
            {
                InteractionText.gameObject.SetActive(false);
            }
        }
    }
}