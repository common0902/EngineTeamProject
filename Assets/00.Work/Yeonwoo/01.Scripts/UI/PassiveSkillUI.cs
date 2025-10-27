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
        [SerializeField] private PassiveData passiveData;
        [field:SerializeField] public TextMeshProUGUI NameText {get; set;}
        [field:SerializeField] public TextMeshProUGUI DescriptionText  {get; set;}
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
            NameText.gameObject.SetActive(false);
            DescriptionText.gameObject.SetActive(false);
            _image.sprite = passiveData.Icon;
            NameText.text = passiveData.Name;
            DescriptionText.text = passiveData.Description;
            Player.Instance.PasiveSkillControllerCompo.OnTakePasiveSkill += ChangeText;
        }

        private void ChangeText(PasiveSkill skill)
        {
            NameText.text += passiveData.Name;
            DescriptionText.text = passiveData.Description;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            NameText.gameObject.SetActive(true);
            DescriptionText.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            NameText.gameObject.SetActive(false);
            DescriptionText.gameObject.SetActive(false);
        }
    }
}