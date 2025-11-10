using System;
using _00.Work.SYH._02Script.ETG;
using _00.Work.Yeonwoo._01.Scripts.Direction;
using _00.Work.Yeonwoo._01.Scripts.UI;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.Yeonwoo._01.Scripts.CutScene
{
    public class PlayerDeathCinema : MonoBehaviour
    {
        private HealthSystem _event;
        private PlayerStateStopper _stopState;
        private DeathCinemaPanel _deathImage;
        [SerializeField] private CinemachineCamera deathCam;

        public Action OnCinemaComplete; // 사망 연출 끝나고 UI 띄우기용
        
        private void Awake()
        {
            _stopState = GetComponent<PlayerStateStopper>();
            _event = GetComponent<HealthSystem>();
            _deathImage = DeathCinemaPanel.Instance;
            Debug.Assert(_deathImage != null, "deathImage is null!");
        }

        private void Start()
        {
            _event.OnDead += DeadCinema;
        }

        private void DeadCinema()
        {
            _stopState.DisableControls();

            var lens = deathCam.Lens;
            
            Sequence deathSequence = DOTween.Sequence();

            if (lens.Orthographic)
            {
                float originalSize = lens.OrthographicSize;
                deathSequence.Append(DOTween.To(
                    () => lens.OrthographicSize,
                    x => { lens.OrthographicSize = x; deathCam.Lens = lens; },
                    1.5f,
                    1f
                ));
            }
            
            deathSequence.AppendInterval(0.3f);
            
            deathSequence.Append(_deathImage.DeathImage.DOFade(1f, 1.5f));
            
            deathSequence.Append(transform.DOScale(new Vector3(0f, 0f, 0f),2.5f));
            
            deathSequence.OnComplete(() =>
            {
                OnCinemaComplete?.Invoke(); // 결과창은 다른 코드에서
                Debug.Log("Death Sequence Complete: Game paused.");
            });
        }

        private void OnDestroy()
        {
            _event.OnDead -= DeadCinema;
        }
    }
}