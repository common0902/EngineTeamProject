using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSpriteContainerListSO", menuName = "Scriptable Objects/PlayerSpriteContainerListSO")]
public class PlayerSpriteContainerListSO : ScriptableObject
{
    public PlayerSpriteContainerSO[] Containers;
}
