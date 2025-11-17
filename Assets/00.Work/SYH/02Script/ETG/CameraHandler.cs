using System.Collections;
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
    public void Zoomin(float size)
    {
        StartCoroutine(ZoominCorutine(size));
    }
    IEnumerator ZoominCorutine(float size)
    {
        while (_followCam.Lens.OrthographicSize > size)
        {
            _followCam.Lens.OrthographicSize -= 20 * Time.deltaTime;
            yield return null;
        }
        _followCam.Lens.OrthographicSize = size;
        yield return null;
    }
    public void Zoomout(float size)
    {
        StartCoroutine(ZoomoutCorutine(size));
    }
    IEnumerator ZoomoutCorutine(float size)
    {
        while (_followCam.Lens.OrthographicSize < size)
        {
            _followCam.Lens.OrthographicSize += 20 * Time.deltaTime;
            yield return null;
        }
        _followCam.Lens.OrthographicSize = size;
        yield return null;
    }
}
