using UnityEngine;

[CreateAssetMenu(fileName = "EnemyLayerContainerSO", menuName = "Scriptable Objects/EnemyLayerContainerSO")]
public class EnemyLayerContainerSO : ScriptableObject
{
    public LayerMask _enemy;
}
