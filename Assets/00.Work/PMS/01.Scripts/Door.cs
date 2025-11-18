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

    private readonly int _lockHash = Animator.StringToHash("IsLock");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

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
        _animator.SetBool(_lockHash, true);
    }

    public void Unlock()
    {
        _isLocked = false;
        _animator.SetBool(_lockHash, false);
    }
}
