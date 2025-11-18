using System;
using System.Linq;
using NavMeshPlus.Components;
using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class NavMeshBakeManager : MonoSingleton<NavMeshBakeManager>
{
    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private NavMeshSurface[] surfaces;

    protected override void Awake()
    {
        base.Awake();
        CheckSurface();
    }

    private void Start()
    {
        Bake();
    }

    private void CheckSurface()
    {
        if (surface == null)
        {
            surfaces = FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.None).ToArray();

            if (surfaces.Length <= 0)
            {
                GameObject addComp = new GameObject();
                addComp.name = "NavMeshSurface";
                surface = addComp.AddComponent<NavMeshSurface>();
                addComp.AddComponent<CollectSources2d>();
                addComp.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
                surface.hideEditorLogs = true;
                surface.agentTypeID = 0;
                surface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
            }
        }
    }
    [ContextMenu("Bake")]
    public void Bake()
    {
        if (surface == null)
        {
            Debug.LogError("NavMeshSurface is null");
            return;
        }

        if (!HasModifierTilemap())
        {
            Debug.LogError("<color=#FF5555>NavMeshModifierTilemap is null</color>");
            return;
        }
        
        try
        {
            surface.BuildNavMesh();
        }
        catch (Exception e)
        {
            Debug.LogError($"<color=#FF5555>BuildNavMesh error</color> {e.Message}");
            return;
        }
    
        var navMeshData = surface.navMeshData;
        if (navMeshData != null)
        {
            Debug.Log("<color=green>NavMesh spawned</color>");
        }
        else
        {
            Debug.LogError("<color=red> NavMesh Bake Failed navMeshData is null</color>");
        }
    }
    private bool HasModifierTilemap()
    {
        var modifiers = FindObjectsByType<NavMeshModifierTilemap>(FindObjectsSortMode.None);
        return modifiers != null && modifiers.Length > 0;
    }
}
