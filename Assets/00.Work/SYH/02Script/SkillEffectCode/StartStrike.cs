using DG.Tweening;
using UnityEngine;

public class StartStrike : MonoBehaviour
{
    [SerializeField] Transform _eruption1;
    [SerializeField] Transform _eruption2;
    private void Update()
    {
        _eruption1.localPosition += new Vector3(0, 16, 0) * Time.deltaTime;
        _eruption2.localPosition += new Vector3(0, 16, 0) * Time.deltaTime;
        if (_eruption1.localPosition.y > 13)
        {
            _eruption1.position -= new Vector3(0, 32, 0);
        }
        if (_eruption2.localPosition.y > 13)
        {
            _eruption2.position -= new Vector3(0, 32, 0);
        }
    }
}
