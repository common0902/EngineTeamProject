using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TypeWriterScripts : MonoBehaviour
{
    [Header("텍스트 컴포")]
    [SerializeField] private TextMeshProUGUI _textUI;

    [Header("텍스트 설정")]
    [SerializeField, TextArea(2, 5)] private string[] _dialogues;
    [SerializeField] private float _typingSpeed = 0.05f;
    [SerializeField] private float _delayBetweenLines = 1.0f;

    [Header("자동 시작 여부")]
    [SerializeField] private bool _playOnStart = true;

    private int _currentIndex = 0;

    private void Start()
    {
        if (_playOnStart)
            StartCoroutine(PlayDialogues());
    }

    public IEnumerator PlayDialogues()
    {
        _textUI.text = "";

        while (_currentIndex < _dialogues.Length)
        {
            yield return StartCoroutine(TypeText(_dialogues[_currentIndex]));
            _currentIndex++;

            yield return new WaitForSeconds(_delayBetweenLines);
        }

        OnDialogueEnd();
    }

    private IEnumerator TypeText(string line)
    {
        _textUI.text = "";

        foreach (char c in line)
        {
            _textUI.text += c;
            yield return new WaitForSeconds(_typingSpeed);
        }
    }

    private void OnDialogueEnd()
    {
        Debug.Log("대사 재생 완료");
        SceneManager.LoadScene(2);
    }
}
