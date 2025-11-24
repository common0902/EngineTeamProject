using UnityEngine;

public class DebugKey : MonoBehaviour
{
    [SerializeField] GameObject _roomManager;
    [SerializeField] GameObject _poolManager;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Player.Instance.PlayerHealthSystemCompo.Damage(99999);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            Instantiate(_roomManager);
            Instantiate(_poolManager);

            Time.timeScale = 1;
            IntroManager.Instance.BlackText.color = new Color(1,1,1, 0);
            IntroManager.Instance._black.color = new Color(1,1,1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            Player.Instance.PlayerStatusCompo._damage += 9900;
        }
    }
}
