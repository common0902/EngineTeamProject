using _00.Work.Yeonwoo._01.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class SkillUI : MonoBehaviour
    {
        [SerializeField] private SkillData skillData;
        [field:SerializeField] public TextMeshProUGUI NameText { get; set; }
        [field:SerializeField] public TextMeshProUGUI DescriptionText { get; set; }
        private Sprite _sprite;
        
        private void Start()
        {
            _sprite = skillData.Icon;
            NameText.text = skillData.Name;
            DescriptionText.text = skillData.Description;
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