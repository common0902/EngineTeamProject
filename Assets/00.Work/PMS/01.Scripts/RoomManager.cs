using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject roomPrefab;

    [Header("room num")]
    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 10;

    [Header("room size")]
    [SerializeField] private int roomWidth = 20;
    [SerializeField] private int roomHeight = 12;

    [Header("room grid size")]
    [SerializeField] private int gridSizeX = 10;
    [SerializeField] private int gridSizeY = 10;

    private List<GameObject> roomObjects = new List<GameObject>();

    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;

    private int roomCount;

    private bool generationComplete = false;

    private void Start()
    {
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    private void Update()
    {
        if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete)
        {
            Vector2Int roomIndex = roomQueue.Dequeue();
            int gridX = roomIndex.x;
            int gridY = roomIndex.y;

            TryGenerateRoom(new Vector2Int(gridX - 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX + 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX, gridY - 1));
            TryGenerateRoom(new Vector2Int(gridX, gridY + 1));
        }
        else if (roomCount < minRooms)
        {
            Debug.Log("RoomCount was less than the minimum amount of rooms. Trying again ");
            RegenerateRooms();
        }
        else if (!generationComplete)
        {
            Debug.Log($"Generation complete, {roomCount} rooms created");
            generationComplete = true;
        }
    }

    
    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        roomQueue.Enqueue(roomIndex);

        roomGrid[x, y] = 1;
        roomCount++;

        var initialRoom = Instantiate(roomPrefab,GetPositionFromGridIndex(roomIndex),Quaternion.identity);

        initialRoom.name = $"Room-{roomCount}"; 
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
    }

    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (roomCount >= maxRooms)
            return false;

        if(Random.value <0.5f && roomIndex != Vector2Int.zero)
            return false;

         if(CountAdjacentRooms(roomIndex) > 1)
            return false;

        roomQueue.Enqueue(roomIndex);

        roomGrid[x, y] = 1;
        roomCount++;

        var newRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);

        newRoom.name = $"Room-{roomCount}";
        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(newRoom);

        OpenDoors(newRoom,x,y);

        return true;
    }
    private void RegenerateRooms() // 지금 있는 방 싹 다 처리하고 다시 생성
    {
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    private void OpenDoors(GameObject room, int x, int y) // void // 문을 생성해주는 함수
    {
        Room newRoomScript = room.GetComponent<Room>();

        // 이웃한 방 Room 스크립트 가져오기
        Room leftRoomScript = GetRoomScriptAt(new Vector2Int(x - 1, y));
        Room rightRoomScript = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room topRoomScript = GetRoomScriptAt(new Vector2Int(x, y + 1));
        Room bottomRoomScript = GetRoomScriptAt(new Vector2Int(x, y - 1));

        if (x > 0 && roomGrid[x - 1, y] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.right);
            leftRoomScript.OpenDoor(Vector2Int.left);
        }
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.left);
            rightRoomScript.OpenDoor(Vector2Int.right);
        }
        if (y > 0 && roomGrid[x, y - 1] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomScript.OpenDoor(Vector2Int.up);
        }
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.up);
            topRoomScript.OpenDoor(Vector2Int.down);
        }
    }

    private Room GetRoomScriptAt(Vector2Int index) // Room // 그 인덱스에 방 스크립트를 가져오는 함수
    {
         GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);
         if (roomObject != null)
             return roomObject.GetComponent<Room>();
         return null;
    }

    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        if(x > 0 && roomGrid[x - 1,y] != 0) count++; // 왼쪽에 인접한 방이 있는지
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) count++; // 오른쪽 확인
        if (y > 0 && roomGrid[x, y - 1] != 0) count++; // 아래쪽 확인
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) count++; // 위쪽 확인

        return count;
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex) // 그 인덱스는 이 위치에 생성해!에서 그 위치를 그리드 인덱스를 알려주면 그걸 알려주는 메서드
    {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        return new Vector3(roomWidth * (gridX - gridSizeX / 2), 
            roomHeight * (gridY - gridSizeY / 2));
    }

    private void OnDrawGizmos() // 디버그용 씬창에서 선으로 방 그리드 표현
    {
        Color gizmoColor = new Color(0, 1f, 1f, 0.05f);
        Gizmos.color = gizmoColor;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x,y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }
}
