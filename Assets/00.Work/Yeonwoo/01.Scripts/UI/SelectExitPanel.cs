using _00.Work.Yeonwoo._01.Scripts.Data;
using _00.Work.Yeonwoo._01.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class SelectExitPanel : MonoSingletonUI<SelectExitPanel>, ISettingPanel
    {
        public bool IsOpen => gameObject.activeSelf;

        public void SelectExit()
        {
            base.Awake();
            Application.Quit();
            // 에디터에선 씬매니저로 대체함. 밑에 코드는 나중에 지우기
            Debug.Log("에디터 전용 게임 종료 시스템. 이 디버그가 보인다면 언젠가는 지우세요.");
            SceneManager.LoadScene("Prolog");
        }

        public void Open() => gameObject.SetActive(true);

        public void Close() => gameObject.SetActive(false);
    }
}