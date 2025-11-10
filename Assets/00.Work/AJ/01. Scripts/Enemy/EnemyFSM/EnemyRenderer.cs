using DG.Tweening;
using UnityEngine;

public class EnemyRenderer : MonoBehaviour, IComponent
{
    private Agent _owner;
    public void Flip(Vector2 value)
    {
        Vector3 currentRotation = _owner.transform.eulerAngles;
        
        if(value.x > 0)
            _owner.transform.rotation = Quaternion.Euler(currentRotation.x, 0, 0);
        else
            _owner.transform.rotation = Quaternion.Euler(currentRotation.x, 180, 0);
    }

    public void Initialize(Agent agent)
    {
        _owner = agent;
    }
}
