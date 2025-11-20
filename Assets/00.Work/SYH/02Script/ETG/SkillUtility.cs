using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
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
    public static Vector2 AimWeapon(Vector2 pos)
    {
        return (MouseInput.Instance.MousePosition - pos).normalized;
    }
    public static IEnumerator PastDelay(Skill type, float value) // �� ��
    {
        OnEndPastDelay += type.EndPastDelay;
        Player.Instance.PlayerMoveCompo._cannotMove = false;
        //Player.Instance.SkillControllerCompo._canUseSkill = false;
        yield return new WaitForSeconds(value);
        Player.Instance.PlayerMoveCompo._cannotMove = true;
        //Player.Instance.SkillControllerCompo._canUseSkill = true;
        OnEndPastDelay?.Invoke();
    }
    public static float CalcurateDamage(float dmg)
    {
        Debug.Log(dmg * Player.Instance.PlayerStatusCompo.Damage / 100f);
        return dmg * Player.Instance.PlayerStatusCompo.Damage/100;
    }
    public static void CalculateAngle(Transform pos, float angle)
    {
        float spreadAngle = 0f;
        
        spreadAngle = UnityEngine.Random.Range(-angle, angle);
        
        Quaternion bulletSpreadAngle = Quaternion.Euler(0, 0, spreadAngle);

        pos.rotation *= bulletSpreadAngle;
    }
    public static bool CanUseSkill(int cost)
    {
        if (Player.Instance.PlayerStatusCompo.Mana >= cost)
        {
            Player.Instance.PlayerStatusCompo.CheckManaState();
            return true;
        }
        return false;
    }

    public static IEnumerator ShakeX(Transform target, float force, float time)
    {
        Vector3 origin = target.position;
        float waitTime = 0;
        int lr = 1;
        while (waitTime < time)
        {
            waitTime += Time.deltaTime + 0.04f;
            target.position = origin + new Vector3(force * lr, 0, 0);
            lr *= -1;
            yield return new WaitForSeconds(0.04f);
        }
        target.position = origin;
    }
    public static IEnumerator ShakeX(CinemachineCamera target, float force, float time)
    {
        Vector3 origin = target.GetComponent<CinemachineFollow>().FollowOffset;
        float waitTime = 0;
        int lr = 1;
        while (waitTime < time)
        {
            waitTime += Time.deltaTime + 0.04f;
            target.GetComponent<CinemachineFollow>().FollowOffset = origin + new Vector3(force * lr, 0, 0);
            lr *= -1;
            yield return new WaitForSeconds(0.04f);
        }
        target.GetComponent<CinemachineFollow>().FollowOffset = origin;
    }

    public static void Shuffle<T>(List<T> list)
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < list.Count; i++)
        {
            int randNum = rand.Next(list.Count);
            T tmp = list[randNum];
            list[randNum] = list[i];
            list[i] = tmp;
        }
    }
    public static void Shuffle<T>(T[] array)
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < array.Length; i++)
        {
            int randNum = rand.Next(array.Length);
            T tmp = array[randNum];
            array[randNum] = array[i];
            array[i] = tmp;
        }
    }
}
