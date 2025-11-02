using Unity.Cinemachine;
using UnityEngine;

public class CameraHandler : MonoSingleton<CameraHandler>
{
    [SerializeField] CinemachineCamera _followCam;
    public void MovedTarget(Transform target)
    {
        _followCam.Target.TrackingTarget = target;
    }
    public void ShakeCamera(float scale, float time)
    {
        StartCoroutine(SkillUtility.ShakeX(_followCam, scale, time));
    }
}
