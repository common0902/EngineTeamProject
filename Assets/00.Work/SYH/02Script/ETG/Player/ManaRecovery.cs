using System;
using UnityEngine;

public class ManaRecovery : MonoBehaviour
{
    bool _recovery;
    private void Start()
    {
        Player.Instance.PlayerMoveCompo.OnDisMoved += () => _recovery = true;
        Player.Instance.PlayerMoveCompo.OnMoved += (float a) => _recovery = false;
    }

    private void Update()
    {
        if (_recovery)
        {
            Player.Instance.PlayerStatusCompo.Mana += Player.Instance.PlayerStatusCompo._manaRecovry * Time.deltaTime;
        }
    }
}
