using UnityEngine;

public class SkillAimming
{
    public static Vector2 AimWeapon(Transform pos)
    {
        Vector2 dir = MouseInput.Instance.MousePosition - (Vector2)pos.position;
        float desireAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        pos.transform.localScale = desireAngle > 90 || desireAngle < -90 ? new Vector3(pos.localScale.x, -pos.localScale.y, 1) : new Vector3(pos.localScale.x, pos.localScale.y, 1);
        pos.rotation = Quaternion.Euler(0, 0, desireAngle);
        return (MouseInput.Instance.MousePosition - (Vector2)pos.transform.position).normalized;
        //if (desireAngle > 45 && desireAngle < 135)
        //{
        //    pos.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
        //}
        //else
        //{
        //    pos.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
        //}
    }
}
