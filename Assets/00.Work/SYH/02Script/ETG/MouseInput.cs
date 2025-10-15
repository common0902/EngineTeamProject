using System;
using UnityEngine;

public class MouseInput : MonoSingleton<MouseInput>
{
    public event Action LeftMouseClick;
    public Vector2 MousePosition { get; private set; }
    public GameObject CollisionGameObject { get; private set; }
    private void Update()//진짜 말 그대로 마우스 좌클릭하면 위치 확인하는 거임
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(MousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                CollisionGameObject = hit.collider.gameObject;
            }
            else
            {
                CollisionGameObject = null;
            }
            LeftMouseClick?.Invoke();
        }
    }
}
