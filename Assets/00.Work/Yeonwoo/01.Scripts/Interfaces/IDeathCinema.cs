using DG.Tweening;

namespace _00.Work.Yeonwoo._01.Scripts.Interfaces
{
    public interface IDeathCinema
    {
        Tween PlayEntrance(float duration, bool useUnscaled = true);
        
        Tween PlayDead(float duration, bool useUnscaled = true);
    }
}