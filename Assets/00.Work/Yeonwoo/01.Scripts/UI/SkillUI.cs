using _00.Work.Yeonwoo._01.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class SkillUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [field: SerializeField] public TextMeshProUGUI NameText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI DescriptionText { get; private set; }
        [field: SerializeField] public Image IconImage { get; private set; }
        [field:SerializeField] public Image CoolTimeImage { get; private set; }
        
        private Skill _boundSkill;

        public SkillData GetCurrentSkillData() => _boundSkill?.Data;
        public bool HasData => _boundSkill != null;
        
        public void SetSkill(Skill skill)
        {
            _boundSkill = skill;

            if (_boundSkill == null)
            {
                IconImage.sprite = null;
                NameText.text = "";
                DescriptionText.text = "";
                if (CoolTimeImage != null) CoolTimeImage.gameObject.SetActive(false);
                return;
            }
            
            var data = _boundSkill.Data;
            if (data != null)
            {
                IconImage.sprite = data.Icon;
                NameText.text = data.Name;
                DescriptionText.text = data.Description;
            }
            else
            {
                IconImage.sprite = null;
                NameText.text = "";
                DescriptionText.text = "";
            }
            
            if (CoolTimeImage != null)
            {
                CoolTimeImage.type = Image.Type.Filled;
                CoolTimeImage.fillMethod = Image.FillMethod.Radial360;
                CoolTimeImage.gameObject.SetActive(_boundSkill.CooldownFillAmount > 0f);
                CoolTimeImage.fillAmount = _boundSkill.CooldownFillAmount;
            }
        }

        private void Start()
        {
            NameText.gameObject.SetActive(false);
            DescriptionText.gameObject.SetActive(false);
            if (CoolTimeImage != null) CoolTimeImage.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_boundSkill == null || CoolTimeImage == null) return;
            
            float fill = _boundSkill.CooldownFillAmount;
            CoolTimeImage.fillAmount = fill;
            CoolTimeImage.gameObject.SetActive(fill > 0f);
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
