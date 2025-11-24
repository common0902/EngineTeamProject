using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSpriteContainerSO", menuName = "Scriptable Objects/PlayerSpriteContainerSO")]
public class PlayerSpriteContainerSO : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] Sprite[] _sprites;
    public string[] _strings;
    public Dictionary<string, Sprite> Sprites { get; private set; }
    public RuntimeAnimatorController _animator;
    private void OnValidate()
    {
        try
        {
            _animator = Resources.Load<RuntimeAnimatorController>($"PlayerRuntimeAnimatorControlleres/{_name}");
        }
        catch
        {
            Debug.LogError("Failed get _animaror.");
        }
        if (Sprites != null) Sprites.Clear();
        else Sprites = new Dictionary<string, Sprite>();

        try
        {
            for (int i = 0; i < _sprites.Length; i++)
            {
                Sprites.Add(_strings[i], _sprites[i]);
            }
        }
        catch
        {

        }
    }
}
