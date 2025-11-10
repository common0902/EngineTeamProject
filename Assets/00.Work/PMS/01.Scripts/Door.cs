using UnityEngine;

public class Door : MonoBehaviour
{
    public Door connectedDoor; // 연결된 반대편 문
    public Vector2Int targetRoomIndex;
    public Vector2Int doorDirection;

    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TeleportPlayer(collision.gameObject);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        if (connectedDoor != null && connectedDoor.spawnPoint != null)
        {
            player.transform.position = connectedDoor.spawnPoint.position;
        }
        
    }
}
