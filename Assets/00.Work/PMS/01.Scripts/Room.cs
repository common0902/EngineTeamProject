using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject topDoor;
    [SerializeField] private GameObject bottomDoor;
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;

    private int enemyCount;
    private bool hasEnemies;
    private bool isCleared = false;
    public Vector2Int RoomIndex {  get; set; }

    private void Start()
    {
        Enemy[] enemies = GetComponentsInChildren<Enemy>();
        enemyCount = enemies.Length;
        hasEnemies = enemyCount > 0;
    }

    public void OnPlayerEnter()
    {
        if (hasEnemies && !isCleared)
        {
            LockAllDoors();
        }
    }
    public void OnEnemyDied()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            isCleared = true;
            UnlockAllDoors();
        }
    }

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
    private void LockAllDoors()
    {
        if (topDoor.activeSelf) topDoor.GetComponent<Door>()?.Lock();
        if (bottomDoor.activeSelf) bottomDoor.GetComponent<Door>()?.Lock();
        if (leftDoor.activeSelf) leftDoor.GetComponent<Door>()?.Lock();
        if (rightDoor.activeSelf) rightDoor.GetComponent<Door>()?.Lock();
    }

    private void UnlockAllDoors()
    {
        if (topDoor.activeSelf) topDoor.GetComponent<Door>()?.Unlock();
        if (bottomDoor.activeSelf) bottomDoor.GetComponent<Door>()?.Unlock();
        if (leftDoor.activeSelf) leftDoor.GetComponent<Door>()?.Unlock();
        if (rightDoor.activeSelf) rightDoor.GetComponent<Door>()?.Unlock();
    }
}
