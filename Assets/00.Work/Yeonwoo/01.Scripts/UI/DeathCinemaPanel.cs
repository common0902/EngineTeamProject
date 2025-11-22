using System;
using _00.Work.Yeonwoo._01.Scripts.CutScene;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class DeathCinemaPanel : MonoSingletonUI<DeathCinemaPanel>
    {
        [SerializeField] private TextMeshProUGUI[] texts;
        [SerializeField] private Button[] buttons;

        private PlayerDeathCinema _cinema;

        protected override void Awake()
        {
            base.Awake();
            var player = GameObject.FindWithTag("Player") ?? GameObject.Find("Player");
            if (player != null)
                _cinema = player.GetComponent<PlayerDeathCinema>();
            else
                Debug.LogWarning("Player not found for DeathCinemaPanel.");
        }

        private void Start()
        {
            if (_cinema != null)
                _cinema.OnCinemaComplete += AppearanceUI;
            else
                Debug.LogWarning("PlayerDeathCinema reference is null in DeathCinemaPanel.");
        }

        private void AppearanceUI()
        {
            Time.timeScale = 0f;

            foreach (var text in texts)
            {
                if (text == null) continue;
                text.gameObject.SetActive(true);
                text.alpha = 0f;
                text.DOFade(1f, 1.5f).SetUpdate(true);
            }

            foreach (var button in buttons)
            {
                if (button == null) continue;
                button.gameObject.SetActive(true);
                if (button.image != null)
                {
                    var col = button.image.color;
                    col.a = 0f;
                    button.image.color = col;
                    button.image.DOFade(1f, 1.5f).SetUpdate(true);
                }
            }
        }

        private void OnDisable()
        {
            if (_cinema != null)
                _cinema.OnCinemaComplete -= AppearanceUI;
        }

        public void Restart()
        {
            SceneManager.LoadScene("Develop");
            Time.timeScale = 1f;
        }

        public void EgenExit()
        {
            SceneManager.LoadScene("Title");
        }
    }
}
