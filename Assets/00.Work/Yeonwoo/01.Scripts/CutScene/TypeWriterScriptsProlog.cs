using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

namespace _00.Work.Yeonwoo._01.Scripts.CutScene
{
    public class TypeWriterScriptsProlog : MonoBehaviour
    {
        [Header("텍스트 컴포")]
        [SerializeField] private TextMeshProUGUI textUI;

        [Header("텍스트 설정")]
        [SerializeField, TextArea(2, 5)] private string[] dialogues;
        [SerializeField] private float typingSpeed = 0.05f;
        [SerializeField] private float delayBetweenLines = 1.0f;

        [SerializeField] private string sceneName;
        
        private int _currentIndex = 0;

        private void Start()
        {
                StartCoroutine(PlayDialogues());
        }

        private IEnumerator PlayDialogues()
        {
            textUI.text = "";

            while (_currentIndex < dialogues.Length)
            {
                yield return StartCoroutine(TypeText(dialogues[_currentIndex]));
                _currentIndex++;

                yield return new WaitForSeconds(delayBetweenLines);
            }

            OnDialogueEnd();
        }

        private void Update()
        {
            SkipKey();
        }

        private void SkipKey()
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(sceneName);
            }
        }

        private IEnumerator TypeText(string line)
        {
            textUI.text = "";

            foreach (char c in line)
            {
                textUI.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        private void OnDialogueEnd()
        {
            Debug.Log("끝");
            SceneManager.LoadScene(sceneName);
        }
    }
}
