using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public abstract class AbstractSkills : MonoBehaviour
    {
        [SerializeField] protected Image icon;
        [SerializeField] protected TextMeshProUGUI nameText;
        [SerializeField] protected TextMeshProUGUI descriptionText;
        [SerializeField] protected TextMeshProUGUI statText;
        [SerializeField] protected TextMeshProUGUI consumptionText;
        [SerializeField] private Image panel;
        
        private float _range = 2f;
        private Transform _playerPos;

        protected virtual void Awake()
        {
            panel.gameObject.SetActive(false);
            
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

        private void Interaction()
        {
            float distance = Vector3.Distance(transform.position, _playerPos.position);

            if (distance <= _range)
            {
                panel.gameObject.SetActive(true);
            }
            else
            {
                panel.gameObject.SetActive(false);
            }
        }
    }
}