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

    public int CurrentWorld { get; private set; } = 1;
    public int CurrentStage { get; private set; } = 1;

    // 현재 플레이어가 있는 방
    public Room CurrentRoom { get; private set; }

    private string SavePath =>
        Path.Combine(Application.persistentDataPath, "stage_save.json");

    protected override void Awake()
    {
        base.Awake();
        LoadStage();
        ApplyStageSettings();
    }

    [ContextMenu("Next")]
    public void OnPortalUsed()
    {
        GoToNextStage();
        SaveStage();
        ApplyStageSettings();
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
        if (RoomManager.Instance == null)
            return;
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
}