using System;
using System.Collections;
using UnityEngine;

public class SkillUtility
{
    public static event Action OnEndPastDelay;
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
    public static IEnumerator PastDelay(Skill type, float value)
    {
        OnEndPastDelay += type.EndPastDelay;
        Player.Instance.PlayerMoveCompo._canMove = false;
        Player.Instance.SkillControllerCompo._canUseSkill = false;
        yield return new WaitForSeconds(value);
        Player.Instance.PlayerMoveCompo._canMove = true;
        Player.Instance.SkillControllerCompo._canUseSkill = true;
        OnEndPastDelay?.Invoke();
    }
    public static float CalcurateDamage(float dmg)
    {
        return dmg * Player.Instance.PlayerStatusCompo._damage/100;
    }
}
