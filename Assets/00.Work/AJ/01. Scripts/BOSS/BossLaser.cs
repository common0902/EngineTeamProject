using DG.Tweening;
using UnityEngine;

namespace _00.Work.AJ._01._Scripts.BOSS
{
    public class BossLaser : MonoBehaviour
    {
        public GameObject laser;

        private void Awake()
        {
            laser.transform.localScale = new Vector3(0f, 1f, 1f);
        }
        [ContextMenu("Laser")]
        public void Init(Vector3 pos)
        {
            CameraHandler.Instance.ShakeCamera(0.03f, 8f);
            transform.position = new Vector3(pos.x - 11f, pos.y, 0f);
            laser.transform.DOScaleX(11.88f, 5f).OnComplete(() => laser.transform.DOScaleX(0f, 1f));
        }
    }
}
