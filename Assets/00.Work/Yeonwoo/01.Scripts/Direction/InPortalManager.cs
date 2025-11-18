using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.Yeonwoo._01.Scripts.Direction
{
    public class InPortalManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _interactionText;
        private readonly float _range = 2f;
        private Transform _playerPos;
        
        public event Action PlayerPortalIn;

        private void OnEnable()
        {
            if (_interactionText) 
                _interactionText.gameObject.SetActive(false);
            GameObject player = GameObject.Find("Player"); // 에디터 이름으로 찾는거라 이름은 Player로 계속 유지
            if (player != null)
            {
                _playerPos = player.transform;
            }
        }

        private void Update()
        {
            Interaction();
        }

        private void Interaction()
        {
            float distance = Vector3.Distance(transform.position, _playerPos.position);

            if (distance <= _range)
            {
                _interactionText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    PlayerPortalIn?.Invoke();
                }
            }
            else
            {
                _interactionText.gameObject.SetActive(false);
            }
        }

        public void NextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                SceneManager.LoadScene("Title");
                return;
            }
        }
    }
}
