using _00.Work.Yeonwoo._01.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class PassiveSkillUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // SkillUI랑 똑같음
    {
        [field: SerializeField] public TextMeshProUGUI NameText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI DescriptionText { get; private set; }
        [field: SerializeField] public Image IconImage { get; private set; }
        
        private PassiveData _data;
        
        public void SetPassiveData(PassiveData data)
        {
            _data = data;

            if (_data == null)
            {
                IconImage.sprite = null;
                NameText.text = "Dataerr0r";
                DescriptionText.text = "";
                return;
            }
            
            IconImage.sprite = _data.Icon;
            NameText.text = _data.Name;
            DescriptionText.text = _data.Description;
        }

        private void Start()
        {
            NameText.gameObject.SetActive(false);
            DescriptionText.gameObject.SetActive(false);
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