using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public Transform Target { get; private set; }
    [field:SerializeField] public Transform FirePos { get; private set; } 
    [SerializeField] private LayerMask playerMask;
    [SerializeField] public LayerMask whatIsWall;
    [field:SerializeField] public float Speed { get; set; } = 1f;
    [field:SerializeField] public float AttackRange { get; private set; }
    
    
    

    public bool CheckAttackRange()
    {
        return Physics2D.OverlapCircle(transform.position, AttackRange, playerMask);
    }

}
