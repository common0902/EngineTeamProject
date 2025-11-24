using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public abstract class AbstractInteractionUI : MonoBehaviour
    {
        [SerializeField] protected Image icon;
        [SerializeField] protected TextMeshProUGUI nameText;
        [SerializeField] protected TextMeshProUGUI descriptionText;
        [SerializeField] protected TextMeshProUGUI statText;
        [SerializeField] protected TextMeshProUGUI staminaText;
    }
}