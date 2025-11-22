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
        [field: SerializeField] public Image CoolTimeImage { get; private set; }

        private Skill _boundSkill;

        public SkillData GetCurrentSkillData() => _boundSkill?.Data;
        public bool HasData => _boundSkill != null && _boundSkill.Data != null;

        private void Awake()
        {
            if (IconImage != null) IconImage.gameObject.SetActive(false);
            if (NameText != null) NameText.gameObject.SetActive(false);
            if (DescriptionText != null) DescriptionText.gameObject.SetActive(false);
            if (CoolTimeImage != null) CoolTimeImage.gameObject.SetActive(false);
        }

        public void SetSkill(Skill skill)
        {
            _boundSkill = skill;
            
            if (_boundSkill == null)
            {
                if (NameText != null) NameText.text = "";
                if (DescriptionText != null) DescriptionText.text = "";
                if (IconImage != null)
                {
                    IconImage.sprite = null;
                    IconImage.gameObject.SetActive(false);
                }
                if (CoolTimeImage != null)
                {
                    CoolTimeImage.sprite = null;
                    CoolTimeImage.gameObject.SetActive(false);
                }
                return;
            }

            var data = _boundSkill.Data;
            if (data != null)
            {
                if (IconImage != null)
                {
                    IconImage.gameObject.SetActive(true);
                    IconImage.sprite = data.Icon;
                }
                if (NameText != null) NameText.text = data.Name ?? "";
                if (DescriptionText != null) DescriptionText.text = data.Description ?? "";
            }
            else
            {
                if (IconImage != null)
                {
                    IconImage.sprite = null;
                    IconImage.gameObject.SetActive(false);
                }
                if (NameText != null) NameText.text = "";
                if (DescriptionText != null) DescriptionText.text = "";
            }
            
            if (CoolTimeImage != null)
            {
                CoolTimeImage.type = Image.Type.Filled;
                CoolTimeImage.fillMethod = Image.FillMethod.Radial360;
                float fill;
                try
                {
                    fill = _boundSkill != null ? _boundSkill.CooldownFillAmount : 0f;
                }
                catch
                {
                    fill = 0f;
                }
                fill = Mathf.Clamp01(fill);
                CoolTimeImage.fillAmount = fill;
                CoolTimeImage.gameObject.SetActive(fill > 0f);
            }
        }

        private void Update()
        {
            if (_boundSkill == null || CoolTimeImage == null) return;

            float fill;
            try
            {
                fill = _boundSkill.CooldownFillAmount;
            }
            catch
            {
                fill = 0f;
            }

            fill = Mathf.Clamp01(fill);
            CoolTimeImage.fillAmount = fill;
            CoolTimeImage.gameObject.SetActive(fill > 0f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!HasData) return;
            if (NameText != null) NameText.gameObject.SetActive(true);
            if (DescriptionText != null) DescriptionText.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (NameText != null) NameText.gameObject.SetActive(false);
            if (DescriptionText != null) DescriptionText.gameObject.SetActive(false);
        }
    }
}
