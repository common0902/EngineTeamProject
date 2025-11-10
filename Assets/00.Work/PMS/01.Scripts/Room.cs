using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject topDoor;
    [SerializeField] private GameObject bottomDoor;
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;

    public Vector2Int RoomIndex {  get; set; }

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
            }
            return doorScript;
        }

        return null;
    }
}
