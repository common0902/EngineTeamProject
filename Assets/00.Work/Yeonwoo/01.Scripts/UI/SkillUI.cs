using _00.Work.Yeonwoo._01.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class SkillUI : MonoBehaviour
    {
        [SerializeField] private SkillData skillData;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        private Sprite _sprite;
        
        private void Start()
        {
            _sprite = skillData.Icon;
            _nameText.text = skillData.Name;
            _descriptionText.text = skillData.Description;
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