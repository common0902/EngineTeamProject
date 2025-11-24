using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [Serializable]
    private class SaveData
    {
        public int world;
        public int stage;
    }

    [field: SerializeField]public int CurrentWorld { get; private set; } = 1;
    [field: SerializeField]public int CurrentStage { get; private set; } = 1;

    // 현재 플레이어가 있는 방 
    public Room CurrentRoom { get; private set; }

    private string SavePath =>
        Path.Combine(Application.persistentDataPath, "stage_save.json");

    [SerializeField] GameObject _boxPrefab;
    GameObject _prefab;
    protected override void Awake()
    {
        base.Awake();
        LoadStage();
        ApplyStageSettings();
        print(111);
        _prefab = Instantiate(_boxPrefab, Vector3.down*2, Quaternion.identity);
    }
    private void Start()
    {
        print(444);
        RoomManager.Instance.OnInPortal += () =>
        {
            print(222);
            if(CurrentStage != 1 || CurrentWorld !=1)
            {
                print(333);
                Destroy(_prefab);
            }
        };
    }

    [ContextMenu("Next")]
    public void OnPortalUsed()
    {
        GoToNextStage();
        SaveStage();
        ApplyStageSettings();
        
        var startSplash = FindAnyObjectByType<_00.Work.Yeonwoo._01.Scripts.UI.GameStartSplashUI>();
        if (startSplash != null)
        {
            startSplash.ShowCurrentStage();
        }
        else
        {
            Debug.LogWarning("[GameManager] GameStartSplashUI 인스턴스를 찾을 수 없습니다.");
        }
    }

    private void GoToNextStage()
    {
        if (CurrentWorld == 1)
        {
            if (CurrentStage < 3)
            {
                CurrentStage++;
            }
            else
            {
                CurrentWorld = 2;
                CurrentStage = 1;
            }
        }
        else if (CurrentWorld == 2)
        {
            if (CurrentStage < 3)
            {
                CurrentStage++;
            }
            else
            {
                CurrentWorld = 2;
                CurrentStage = 3;
            }
        }
    }

    public void SaveStage()
    {
        SaveData data = new SaveData
        {
            world = CurrentWorld,
            stage = CurrentStage
        };
        string json = JsonUtility.ToJson(data, true);
        try
        {
            File.WriteAllText(SavePath, json);
        }
        catch (Exception)
        {
        }
    }

    public void LoadStage()
    {
        if (!File.Exists(SavePath))
        {
            CurrentWorld = 1;
            CurrentStage = 1;
            return;
        }
        try
        {
            string json = File.ReadAllText(SavePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            if (data == null)
            {
                CurrentWorld = 1;
                CurrentStage = 1;
                return;
            }
            CurrentWorld = Mathf.Clamp(data.world, 1, 2);
            CurrentStage = Mathf.Clamp(data.stage, 1, 3);
        }
        catch (Exception)
        {
            CurrentWorld = 1;
            CurrentStage = 1;
        }
    }

    private void ApplyStageSettings()
    {
        bool isBossStage = (CurrentStage == 3);
        RoomManager.Instance.BossRoomTurn = isBossStage;
    }

    public void SetCurrentRoom(Room room)
    {
        CurrentRoom = room;
    }

    public Vector3 GetCurrentRoomCenter()
    {
        return CurrentRoom != null ? CurrentRoom.transform.position : Vector3.zero;
    }
    [ContextMenu("ResetSave")]
    public void ResetSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }

        CurrentWorld = 1;
        CurrentStage = 1;

        ApplyStageSettings();

    }

    [ContextMenu("NewGame")]
    public void NewGame()
    {
        CurrentWorld = 1;
        CurrentStage = 1;

        SaveStage();          // 1-1로 세이브 덮어쓰기
        ApplyStageSettings(); // 보스 여부 등 반영
        RoomManager.Instance.RegenerateRooms(); // 맵도 새로 생성
        
    }
}