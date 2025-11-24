using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(-200)]
public class RoomManager : MonoSingleton<RoomManager>
{
    [Header("normal Room Prefabs")]
    [SerializeField] private GameObject startRoomPrefab;
    [SerializeField] private GameObject portalRoomPrefab;
    [SerializeField] private GameObject bossRoomPrefab;
    [SerializeField] private GameObject[] goldRoomPrefab;
    [SerializeField] private GameObject[] roomPrefab;
    [SerializeField] private GameObject[] shopRoomPrefab;

    [Header("purple Room Prefabs")]
    [SerializeField] private GameObject startRoomPurplePrefab;
    [SerializeField] private GameObject portalRoomPurplePrefab;
    [SerializeField] private GameObject bossRoomPurplePrefab;
    [SerializeField] private GameObject[] goldRoomPurplePrefab;
    [SerializeField] private GameObject[] roomPurplePrefab;
    [SerializeField] private GameObject[] shopRoomPurplePrefab;


    [Header("room num")]
    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 10;
    [SerializeField] private int roomsIncreasePerStage = 2;

    private int baseMaxRooms;
    private int baseMinRooms;

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

    [Header("last room")]
    [SerializeField] private bool lastRoomGeneration = true;
    private bool lastRoomCreated = false;

    [Header("room index")]
    public int goldRoomIndex = 0;
    public int shopRoomIndex = 0;
    [SerializeField] bool _useIt;
    [field:SerializeField]public bool BossRoomTurn { get; set; } = false;
    public event Action OnInPortal;
    public Action OnSetComplete;
    protected override void Awake()
    {
        if (!_useIt)
        {
            Destroy(gameObject);
            return;
        }
        base.Awake();
    }
    private void Start()
    {
        baseMaxRooms = maxRooms;
        baseMinRooms = minRooms;

        ApplyStageOption();
        RandomIndex();

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

            if (roomGrid[gridX, gridY] != 1)
                return;

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
        else if (lastRoomGeneration)
        {
            if (roomQueue.Count > 0)
                LastRoomGeneration();
            else
                RegenerateRooms();
        }
        else if (!lastRoomCreated)  
        {
            RegenerateRooms();
        }
        else if (!generationComplete)
        {
            Debug.Log($"Generation complete, {roomCount} rooms created");
            MapGenerationComplete();
        }
        
    }

    private void MapGenerationComplete()
    {
        try
        {
            OnInPortal?.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogError($"OnInPortal 이벤트 처리 중 예외 발생: {ex}");
        }
        OnSetComplete?.Invoke();
        NavMeshBakeManager.Instance.Bake();
        generationComplete = true;
    }

    private void LastRoomGeneration()
    {

        Vector2Int roomIndex = roomQueue.Dequeue();
        int gridX = roomIndex.x;
        int gridY = roomIndex.y;

        if (roomGrid[gridX, gridY] != 1)
            return;

        if (TryGenerateLastRoom(new Vector2Int(gridX - 1, gridY)))
            return;
        else if (TryGenerateLastRoom(new Vector2Int(gridX + 1, gridY)))
            return;
        else if (TryGenerateLastRoom(new Vector2Int(gridX, gridY - 1)))
            return;
        else if (TryGenerateLastRoom(new Vector2Int(gridX, gridY + 1)))
            return;
        
    }

    private void RandomIndex()
    {
        int offSet = maxRooms / 2;
        goldRoomIndex = Random.Range(2, offSet);
        shopRoomIndex = Random.Range(offSet, offSet * 2);
    }
    
    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        roomQueue.Enqueue(roomIndex);

        roomGrid[x, y] = 1;
        roomCount++;

        var initialRoom = Instantiate(startRoomPrefab,GetPositionFromGridIndex(roomIndex),Quaternion.identity);

        initialRoom.name = $"Room-{roomCount}"; 
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
    }

    private bool TryGenerateLastRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (x >= gridSizeX || y >= gridSizeY || x < 0 || y < 0)
            return false;

        if (CountAdjacentRooms(roomIndex) > 1)
            return false;

        if (roomGrid[x, y] != 0)
            return false;

        GameObject lastRoom;

        if (BossRoomTurn)
            lastRoom = Instantiate(bossRoomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        else
            lastRoom = Instantiate(portalRoomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);


        lastRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(lastRoom);

        OpenDoors(lastRoom, x, y);

        lastRoomGeneration = false;
        lastRoomCreated = true;

        return true;
    }
    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (roomCount >= maxRooms)
            return false;

        if(Random.value <0.5f && roomIndex != Vector2Int.zero)
            return false;

        if (x >= gridSizeX || y >= gridSizeY || x < 0 || y < 0)
            return false;

        if(CountAdjacentRooms(roomIndex) > 1)
            return false;

        if (roomGrid[x, y] != 0)
            return false;


        roomQueue.Enqueue(roomIndex);

        roomCount++;

        var newRoom = SetSpecialRoom(roomIndex, x, y);

        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(newRoom);

        OpenDoors(newRoom,x,y);

        return true;
    }

    private GameObject SetSpecialRoom(Vector2Int roomIndex, int x, int y) 
    { 
        GameObject newRoom; 
        int rand; roomGrid[x, y] = 2; 
        if (roomCount == goldRoomIndex) 
        { 
            rand = Random.Range(0, goldRoomPrefab.Length); 
            newRoom = Instantiate(goldRoomPrefab[rand], GetPositionFromGridIndex(roomIndex), Quaternion.identity); 
            newRoom.name = "goldRoom"; 
        } 
        else if (roomCount == shopRoomIndex) 
        { 
            rand = Random.Range(0, shopRoomPrefab.Length);
            newRoom = Instantiate(shopRoomPrefab[rand], GetPositionFromGridIndex(roomIndex), Quaternion.identity); 
            newRoom.name = "shopRoom"; } 
        else 
        {
            rand = Random.Range(0, roomPrefab.Length); 
            newRoom = Instantiate(roomPrefab[rand], GetPositionFromGridIndex(roomIndex), Quaternion.identity); 
            newRoom.name = $"Room-{roomCount}"; 
            roomGrid[x, y] = 1; 
        }
        return newRoom; 
    }
    [ContextMenu("ReGenerationRoom")]
    public void RegenerateRooms()
    {
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;
        lastRoomGeneration = true;
        lastRoomCreated = false;

        ApplyStageOption();   
        RandomIndex();

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    private void OpenDoors(GameObject room, int x, int y)// void // 문을 생성해주는 함수
    {
        Room newRoomScript = room.GetComponent<Room>();

        // 이웃한 방 Room 스크립트 가져오기
        Room leftRoomScript = GetRoomScriptAt(new Vector2Int(x - 1, y));
        Room rightRoomScript = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room topRoomScript = GetRoomScriptAt(new Vector2Int(x, y + 1));
        Room bottomRoomScript = GetRoomScriptAt(new Vector2Int(x, y - 1));

        // 왼쪽 방과 연결
        if (x > 0 && roomGrid[x - 1, y] != 0)
        {
            Door newDoor = newRoomScript.OpenDoor(Vector2Int.right);
            Door leftDoor = leftRoomScript.OpenDoor(Vector2Int.left);

            // 두 문을 서로 연결
            if (newDoor != null && leftDoor != null)
            {
                newDoor.connectedDoor = leftDoor;
                leftDoor.connectedDoor = newDoor;

                newDoor.targetRoomIndex = leftRoomScript.RoomIndex;
                leftDoor.targetRoomIndex = newRoomScript.RoomIndex;

                newDoor.RefreshMarkByConnectedRoom();
                leftDoor.RefreshMarkByConnectedRoom();
            }
        }

        // 오른쪽 방과 연결
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0)
        {
            Door newDoor = newRoomScript.OpenDoor(Vector2Int.left);
            Door rightDoor = rightRoomScript.OpenDoor(Vector2Int.right);

            if (newDoor != null && rightDoor != null)
            {
                newDoor.connectedDoor = rightDoor;
                rightDoor.connectedDoor = newDoor;

                newDoor.targetRoomIndex = rightRoomScript.RoomIndex;
                rightDoor.targetRoomIndex = newRoomScript.RoomIndex;

                newDoor.RefreshMarkByConnectedRoom();
                rightDoor.RefreshMarkByConnectedRoom();
            }
        }

        // 아래쪽 방과 연결
        if (y > 0 && roomGrid[x, y - 1] != 0)
        {
            Door newDoor = newRoomScript.OpenDoor(Vector2Int.down);
            Door bottomDoor = bottomRoomScript.OpenDoor(Vector2Int.up);

            if (newDoor != null && bottomDoor != null)
            {
                newDoor.connectedDoor = bottomDoor;
                bottomDoor.connectedDoor = newDoor;

                newDoor.targetRoomIndex = bottomRoomScript.RoomIndex;
                bottomDoor.targetRoomIndex = newRoomScript.RoomIndex;

                newDoor.RefreshMarkByConnectedRoom();
                bottomDoor.RefreshMarkByConnectedRoom();
            }
        }

        // 위쪽 방과 연결
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0)
        {
            Door newDoor = newRoomScript.OpenDoor(Vector2Int.up);
            Door topDoor = topRoomScript.OpenDoor(Vector2Int.down);

            if (newDoor != null && topDoor != null)
            {
                newDoor.connectedDoor = topDoor;
                topDoor.connectedDoor = newDoor;

                newDoor.targetRoomIndex = topRoomScript.RoomIndex;
                topDoor.targetRoomIndex = newRoomScript.RoomIndex;

                newDoor.RefreshMarkByConnectedRoom();
                topDoor.RefreshMarkByConnectedRoom();
            }
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

    private void ApplyStageOption()
    {
        if (GameManager.Instance == null)
            return;

        int world = GameManager.Instance.CurrentWorld;  
        int stage = GameManager.Instance.CurrentStage;  

        int stageIndex = (world - 1) * 3 + (stage - 1);

        maxRooms = baseMaxRooms + roomsIncreasePerStage * stageIndex;
        minRooms = baseMinRooms + roomsIncreasePerStage * stageIndex;

        if (world >= 2)
        {
            if (startRoomPurplePrefab != null) startRoomPrefab = startRoomPurplePrefab;
            if (portalRoomPurplePrefab != null) portalRoomPrefab = portalRoomPurplePrefab;
            if (bossRoomPurplePrefab != null) bossRoomPrefab = bossRoomPurplePrefab;

            if (goldRoomPurplePrefab != null && goldRoomPurplePrefab.Length > 0)
                goldRoomPrefab = goldRoomPurplePrefab;

            if (roomPurplePrefab != null && roomPurplePrefab.Length > 0)
                roomPrefab = roomPurplePrefab;

            if (shopRoomPurplePrefab != null && shopRoomPurplePrefab.Length > 0)
                shopRoomPrefab = shopRoomPurplePrefab;
        }
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
