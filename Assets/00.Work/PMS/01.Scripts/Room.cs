using UnityEngine;

public enum RoomType
{
    Normal,
    Gold,
    Shop,
    Boss,
    Portal
}

public abstract class Room : MonoBehaviour
{
    [Header("Doors")]
    [SerializeField] private GameObject topDoor;
    [SerializeField] private GameObject bottomDoor;
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;

    public RoomType roomType;

    protected int enemyCount;
    protected bool hasEnemies;
    protected bool isCleared = false;

    public Vector2Int RoomIndex { get; set; }

    protected virtual void Start()
    {
        Enemy[] enemies = GetComponentsInChildren<Enemy>();
        enemyCount = enemies.Length;
        hasEnemies = enemyCount > 0;
    }

    public virtual void OnPlayerEnter()
    {
        if (hasEnemies && !isCleared)
        {
            LockAllDoors();
        }
    }

    public virtual void OnEnemyDied()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            isCleared = true;
            UnlockAllDoors();
        }
    }

    [ContextMenu("문 다 열림")]
    void OpenDoorCheat() => UnlockAllDoors();

    public Door OpenDoor(Vector2Int direction)
    {
        GameObject doorObject = null;

        if (direction == Vector2Int.up)
        {
            topDoor.SetActive(true);
            doorObject = topDoor;
        }
        else if (direction == Vector2Int.down)
        {
            bottomDoor.SetActive(true);
            doorObject = bottomDoor;
        }
        else if (direction == Vector2Int.right)
        {
            rightDoor.SetActive(true);
            doorObject = rightDoor;
        }
        else if (direction == Vector2Int.left)
        {
            leftDoor.SetActive(true);
            doorObject = leftDoor;
        }

        if (doorObject != null)
        {
            Door doorScript = doorObject.GetComponent<Door>();
            if (doorScript != null)
            {
                doorScript.doorDirection = direction;
                doorScript.targetRoomIndex = RoomIndex;
                doorScript.parentRoom = this;
            }
            return doorScript;
        }

        return null;
    }

    protected void LockAllDoors()
    {
        if (topDoor != null && topDoor.activeSelf) topDoor.GetComponent<Door>()?.Lock();
        if (bottomDoor != null && bottomDoor.activeSelf) bottomDoor.GetComponent<Door>()?.Lock();
        if (leftDoor != null && leftDoor.activeSelf) leftDoor.GetComponent<Door>()?.Lock();
        if (rightDoor != null && rightDoor.activeSelf) rightDoor.GetComponent<Door>()?.Lock();
    }

    protected void UnlockAllDoors()
    {
        if (topDoor != null && topDoor.activeSelf) topDoor.GetComponent<Door>()?.Unlock();
        if (bottomDoor != null && bottomDoor.activeSelf) bottomDoor.GetComponent<Door>()?.Unlock();
        if (leftDoor != null && leftDoor.activeSelf) leftDoor.GetComponent<Door>()?.Unlock();
        if (rightDoor != null && rightDoor.activeSelf) rightDoor.GetComponent<Door>()?.Unlock();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPlayerEnter();
            GameManager.Instance?.SetCurrentRoom(this);
        }
    }
}
