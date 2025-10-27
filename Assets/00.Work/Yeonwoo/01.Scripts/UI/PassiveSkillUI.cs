using System;
using _00.Work.Yeonwoo._01.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class PassiveSkillUI : MonoBehaviour
    {
        private PassiveData passiveData;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        private Image _image;

        private void Awake()
        {
            passiveData = FindFirstObjectByType<PassiveData>();
            _image = GetComponent<Image>();
            if (!_image)
                Debug.LogError("스없");
        }

        private void Start()
        {
            _nameText.gameObject.SetActive(false);
            _descriptionText.gameObject.SetActive(false);
            _image.sprite = passiveData.Icon;
            _nameText.text = passiveData.Name;
            _descriptionText.text = passiveData.Description;
            Player.Instance.PasiveSkillControllerCompo.OnTakePasiveSkill += ChangeText;
        }

        private void ChangeText(PasiveSkill skill)
        {
            _nameText.text += passiveData.Name;
            _descriptionText.text = passiveData.Description;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _nameText.gameObject.SetActive(true);
            _descriptionText.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _nameText.gameObject.SetActive(false);
            _descriptionText.gameObject.SetActive(false);
        }
    }
}