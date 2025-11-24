using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private Vector2 roomSize = new Vector2(24, 16);

    [SerializeField] private float enemyActivationDelay = 0.1f;

    public RoomType roomType;

    protected int enemyCount;
    protected bool hasEnemies;
    protected bool isCleared = false;
    private bool enemiesActivated = false;

    public Vector2Int RoomIndex { get; set; }

    private BoxCollider2D roomTrigger;
    private List<Enemy> enemies = new List<Enemy>();
    protected virtual void Awake()
    {
        roomTrigger = gameObject.GetComponent<BoxCollider2D>();
        if (roomTrigger == null)
        {
            roomTrigger = gameObject.AddComponent<BoxCollider2D>();
        }

        roomTrigger.isTrigger = true;
        roomTrigger.size = roomSize;
        roomTrigger.offset = Vector2.zero;
    }
    protected virtual void Start()
    {
        Enemy[] foundEnemies = GetComponentsInChildren<Enemy>(false); // false = 활성화된 것만
        enemies.AddRange(foundEnemies);

        enemyCount = enemies.Count;
        hasEnemies = enemyCount > 0;

        // 모든 에너미 비활성화
        foreach (Enemy enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    public virtual void OnPlayerEnter()
    {
        if (hasEnemies && !enemiesActivated)
        {
            StartCoroutine(ActivateEnemiesWithDelay());
        }
        if (hasEnemies && !isCleared)
        {
            LockAllDoors();
        }
    }
    private IEnumerator ActivateEnemiesWithDelay()
    {
        Player.Instance.PlayerMoveCompo._cannotMove = true;
        yield return new WaitForSeconds(enemyActivationDelay);

        Player.Instance.PlayerMoveCompo._cannotMove = false;
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.gameObject.SetActive(true);
            }
        }

        enemiesActivated = true;
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
