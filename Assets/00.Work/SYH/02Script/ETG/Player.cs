using UnityEngine;

public class Player : MonoSingleton<Player>
{
    [field: SerializeField] public SkillController SkillControllerCompo { get; private set; }
    [field: SerializeField] public PasiveSkillController PasiveSkillControllerCompo { get; private set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
