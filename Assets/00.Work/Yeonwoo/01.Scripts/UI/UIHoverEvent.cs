using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f)
            .SetUpdate(true);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.2f)
            .SetUpdate(true);
    }
}
