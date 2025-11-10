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

        // 현재 이 UI가 표시 중인 SkillData 정보 저장
        private SkillData _data;
        
        // 외부에서 SkillData를 전달받아 UI를 갱신하는 함수
        public void SetSkillData(SkillData data)
        {
            _data = data;

            if (_data == null)
            {
                // 스킬이 비어 있을 경우 초기화
                IconImage.sprite = null;
                NameText.text = "";
                DescriptionText.text = "";
                return;
            }

            // SO에 저장된 정보로 UI 갱신
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