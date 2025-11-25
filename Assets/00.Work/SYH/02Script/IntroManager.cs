using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroManager : MonoSingleton<IntroManager>
{

    public Image _white;
    public Image _black;
    public TextMeshProUGUI BlackText;
    [SerializeField] float _waitTime;
    [SerializeField] float _coolTime = 0.5f;
    //CutScene _cutScene;
    PlayerMove _pl;
    bool _isGameOver;
    [SerializeField] bool _aaa;

    private void Start()
    {
        if (!_aaa)
        {
            Time.timeScale = 0;
            StartCoroutine(Show(0.01f, _black));
            StartCoroutine(Show(0.01f, BlackText));
            RoomManager.Instance.OnInPortal += () =>
            {
                Time.timeScale = 1;
                StartCoroutine(Hide(1f, BlackText));
                StartCoroutine(Hide(1f, _black));
            };
        }
    }
    public IEnumerator Hide(float time, SpriteRenderer sprite)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        while (sprite.color.a > 0f)
        {
            if (_waitTime < _coolTime)
            {
                _waitTime += Time.deltaTime;
            }
            else
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - (Time.deltaTime / time));
                
            }
            yield return null;
            //���� ���������ϱ� �ٽ� �Ⱥ��̰� �ϴ� �ڵ�
        }
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
    }
    public IEnumerator Hide(float time, Image sprite)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        while (sprite.color.a > 0f)
        {
            if (_waitTime < _coolTime)
            {
                _waitTime += Time.deltaTime;
            }
            else
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - (Time.deltaTime / time));

            }
            yield return null;
            //���� ���������ϱ� �ٽ� �Ⱥ��̰� �ϴ� �ڵ�
        }
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
    }
    public IEnumerator Show(float time, SpriteRenderer sprite)
    {
        while (sprite.color.a < 1.0f)
        {
            //�̹����� �Ⱥ��̴� ���¿��� ���̰� �ϴ� �ڵ�
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + (Time.deltaTime / time));
            yield return null;
        }
    }
    public IEnumerator Show(float time, Image sprite)
    {
        while (sprite.color.a < 1.0f)
        {
            //�̹����� �Ⱥ��̴� ���¿��� ���̰� �ϴ� �ڵ�
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + (Time.deltaTime / time));
            yield return null;
        }
    }

    public IEnumerator Show(float time, TextMeshProUGUI sprite)
    {
        while (sprite.color.a < 1.0f)
        {
            //�̹����� �Ⱥ��̴� ���¿��� ���̰� �ϴ� �ڵ�
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + (Time.deltaTime / time));
            yield return null;
        }
    }

    public IEnumerator Hide(float time, TextMeshProUGUI sprite)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        while (sprite.color.a > 0f)
        {
            if (_waitTime < _coolTime)
            {
                _waitTime += Time.deltaTime;
            }
            else
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - (Time.deltaTime / time));

            }
            yield return null;
            //���� ���������ϱ� �ٽ� �Ⱥ��̰� �ϴ� �ڵ�
        }
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
    }
}