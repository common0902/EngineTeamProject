    using UnityEngine;

[RequireComponent (typeof(Animator))]
public class Door : MonoBehaviour
{
    

    public Door connectedDoor; // 연결된 반대편 문
    public Vector2Int targetRoomIndex;
    public Vector2Int doorDirection;
    [HideInInspector] public Room parentRoom;

    [SerializeField] private Transform spawnPoint;
    private bool _isLocked = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_isLocked)
        {
            TeleportPlayer(collision.gameObject);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        if (connectedDoor != null && connectedDoor.spawnPoint != null)
        {
            player.transform.position = connectedDoor.spawnPoint.position;

            connectedDoor.parentRoom?.OnPlayerEnter();
        }
    }

    public void Lock()
    {
        _isLocked = true;
        // 문 닫는 애니메이션 재생
    }

    public void Unlock()
    {
        _isLocked = false;
        // 문 여는 애니메이션 재생
    }
}
