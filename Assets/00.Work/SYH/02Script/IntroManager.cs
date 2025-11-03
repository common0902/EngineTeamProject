using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroManager : MonoSingleton<IntroManager>
{

    [SerializeField] Image _image;
    [SerializeField] float _waitTime;
    [SerializeField] float _coolTime = 0.5f;
    //CutScene _cutScene;
    PlayerMove _pl;
    bool _isGameOver;
    //private void Awake()
    //{
    //    //_cutScene = gameObject.GetComponent<CutScene>();
    //    if (gameObject.name == "GameOver") _isGameOver = true;
    //}

    //void Start()
    //{
    //    if (!_isGameOver)
    //    {
    //        _pl = GameObject.Find("Player")?.GetComponent<PlayerMove>();
    //        if(_pl!=null) _pl._canMove = false;
    //        StartCoroutine(Hide(1));
    //    }
    //}
    public void ShowScene()
    {
        StartCoroutine(Show(0.5f));
    }

    public IEnumerator Hide(float time)
    {
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1);
        while (_image.color.a > 0f)
        {
            if (_waitTime < _coolTime)
            {
                _waitTime += Time.deltaTime;
            }
            else
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a - (Time.deltaTime / time));
                
            }
            yield return null;
            //이제 보여줬으니깐 다시 안보이게 하는 코드
        }
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
        //StartCoroutine(_cutScene.TextPrint());
    }

    public IEnumerator Show(float time)
    {
        while (_image.color.a < 1.0f)
        {
            //이미지가 안보이는 상태에서 보이게 하는 코드
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a + (Time.deltaTime / time));
            yield return null;
        }
        //pl.position = tarPos;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}