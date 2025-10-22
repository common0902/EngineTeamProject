using System;
using _00.Work.Yeonwoo._01.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class PassiveSkillUI : MonoBehaviour
    {
        [SerializeField] private PassiveData passiveData;
        private TextMeshProUGUI _nameText;
        private TextMeshProUGUI _descriptionText;
        private Sprite _sprite;

        private void Awake()
        {
            _nameText = GetComponentInChildren<TextMeshProUGUI>();
            _descriptionText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            _sprite = passiveData.Icon;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //_nameText = passiveData.Name; + setActive
            //_descriptionText = passiveData.Description; + setActive
            // 이거는 내일~~
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }
    }
}