using UnityEngine;

public interface IDamagable
{
    /// <summary>
    /// 데미지 받음.
    /// </summary>
    /// <param name="damage">얼만큼 받았는지</param>
    /// <param name="target">누구한테 받았는지</param>
    public void GetDamage(float damage, GameObject target);
}
