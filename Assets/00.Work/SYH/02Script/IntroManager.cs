using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroManager : MonoSingleton<IntroManager>
{

    public Image _white;
    public Image _black;
    [SerializeField] float _waitTime;
    [SerializeField] float _coolTime = 0.5f;
    //CutScene _cutScene;
    PlayerMove _pl;
    bool _isGameOver;

    private void Start()
    {
        RoomManager.Instance.OnInPortal += () =>
        {
            StartCoroutine(Hide(0.01f, _black));
        };
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
            //이제 보여줬으니깐 다시 안보이게 하는 코드
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
            //이제 보여줬으니깐 다시 안보이게 하는 코드
        }
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
    }
    public IEnumerator Show(float time, SpriteRenderer sprite)
    {
        while (sprite.color.a < 1.0f)
        {
            //이미지가 안보이는 상태에서 보이게 하는 코드
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + (Time.deltaTime / time));
            yield return null;
        }
    }
    public IEnumerator Show(float time, Image sprite)
    {
        while (sprite.color.a < 1.0f)
        {
            //이미지가 안보이는 상태에서 보이게 하는 코드
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + (Time.deltaTime / time));
            yield return null;
        }
    }
}