using System;
using TMPro;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public abstract class AbstractSkills : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI nameText;
        [SerializeField] protected TextMeshProUGUI descriptionText;
        private float _range = 2f;
        private Transform _playerPos;

        protected virtual void Awake()
        {
            nameText.gameObject.SetActive(false);
            descriptionText.gameObject.SetActive(false);
            
            GameObject player = GameObject.Find("Player"); // 에디터 이름으로 찾는거라 이름은 Player로 계속 유지
            if (player != null)
            {
                _playerPos = player.transform;
            }
        }

        protected virtual void Update()
        {
            Interaction();
        }

        protected virtual void Interaction()
        {
            float distance = Vector3.Distance(transform.position, _playerPos.position);

            if (distance <= _range)
            {
                nameText.gameObject.SetActive(true);
                descriptionText.gameObject.SetActive(true);
            }
            else
            {
                nameText.gameObject.SetActive(false);
                descriptionText.gameObject.SetActive(false);
            }
        }
    }
}