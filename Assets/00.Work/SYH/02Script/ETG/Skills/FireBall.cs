using UnityEngine;

public class FireBall : Skill
{
    public override void Active()
    {
        base.Active();
        FireBallPrefab fireBall = Instantiate(SkillPrefab, transform.position, Quaternion.identity).GetComponent<FireBallPrefab>();
        fireBall.transform.position = Player.Instance.transform.position;
        fireBall._targetPos = MouseInput.Instance.MousePosition;
        fireBall._shotSpeed = ShotSpeed;
        fireBall._damage = Damage;
        fireBall._duration = Duration;
    }
}
