using UnityEngine;

public class DebugKey : MonoBehaviour
{
    [SerializeField] GameObject _roomManager;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Player.Instance.PlayerHealthSystemCompo.Damage(99999);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            Instantiate(_roomManager);

            Time.timeScale = 1;
            IntroManager.Instance.BlackText.color = new Color(1,1,1, 0);
            IntroManager.Instance._black.color = new Color(1,1,1, 0);
        }
    }
}
