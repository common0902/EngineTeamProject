using System;
using _00.Work.Yeonwoo._01.Scripts.CutScene;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class DeathCinemaPanel : MonoSingletonUI<DeathCinemaPanel>
    {
        public Image DeathImage;
        private PlayerDeathCinema _cinema;
        [SerializeField] private TextMeshProUGUI[] texts;
        [SerializeField] private Button[] buttons;

        protected override void Awake()
        {
            base.Awake();
            _cinema = GameObject.Find("Player").GetComponent<PlayerDeathCinema>();
        }

        private void Start()
        {
            _cinema.OnCinemaComplete += AppearanceUI;
        }

        private void AppearanceUI()
        {
            foreach (TextMeshProUGUI text in texts)
            {
                text.gameObject.SetActive(true);
                text.DOFade(1f, 1.5f);
            }

            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(true);
                button.image.DOFade(1f, 1.5f);
            }
        }

        private void OnDisable()
        {
            _cinema.OnCinemaComplete -= AppearanceUI;
        }

        public void Restart()
        {
            SceneManager.LoadScene("Develop");
        }

        public void EgenExit()
        {
            Application.Quit();
        }
    }
}