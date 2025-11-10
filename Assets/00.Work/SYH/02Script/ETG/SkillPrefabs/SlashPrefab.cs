using UnityEngine;

public class SlashPrefab : SkillPrefab
{
    public bool _canAttack;
    int _flip = 1;  
    Vector3 _pos;
    Collider2D _collider;
    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }
    private void OnEnable()
    {
        _pos = 1.5f * (Vector3)SkillUtility.AimWeapon(transform);
        _collider.enabled = true;
    }

    public void Hide()
    {
        transform.localScale = new Vector3(1, _flip, 1);
        _canAttack = true;
        _flip = -_flip;
        gameObject.SetActive(false);
    }
    public void DisEnableCollider()
    {
        _collider.enabled = false;
    }
    protected override void Update()
    {
        //transform.position = _pos + (Vector3)SkillUtility.AimWeapon((Vector2)transform.position);
        transform.localPosition = _pos;
    }

}