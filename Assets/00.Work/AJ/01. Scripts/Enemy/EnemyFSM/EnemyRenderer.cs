using DG.Tweening;
using UnityEngine;

public class EnemyRenderer : MonoBehaviour, IComponent
{
    private Agent _owner;
    public void Flip(Vector2 value)
    {
        if(value.x > 0)
            _owner.transform.rotation = Quaternion.Euler(_owner.transform.rotation.x, 0, 0);
        else
            _owner.transform.rotation = Quaternion.Euler(_owner.transform.rotation.x, 180, 0);
    }

    public void Initialize(Agent agent)
    {
        _owner = agent;
    }
}
