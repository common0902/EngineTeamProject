using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager2 : MonoBehaviour
{
    public Image _white;
    public Image _black;
    [SerializeField] float _waitTime;
    [SerializeField] float _coolTime = 0.5f;

    private void Start()
    {
        StartCoroutine(Hide(_coolTime, _black));
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
        }
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
    }
}
