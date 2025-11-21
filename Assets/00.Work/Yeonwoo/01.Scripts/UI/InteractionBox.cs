using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class InteractionBox : MonoBehaviour
    {
        [SerializeField] private float interactionRange = 1.25f;
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

            if (distance <= interactionRange)
            {
                InteractionText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(OpenWait());
                }
            }
            else
            {
                InteractionText.gameObject.SetActive(false);
            }
        }

        private IEnumerator OpenWait()
        {
            yield return null;
            OnBoxOpen?.Invoke();
            this.enabled = false;
        }
    }
}