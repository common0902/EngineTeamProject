using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("마진");
        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.15f)
            .SetUpdate(true);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("마탈");
        transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.15f)
            .SetUpdate(true);
    }
}
