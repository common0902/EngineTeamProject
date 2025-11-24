using UnityEngine;

public class UtilityCorutineManager : MonoSingleton<UtilityCorutineManager>
{
    public void StartPastDelay(Skill type, float value)
    {
        StartCoroutine(SkillUtility.PastDelay(type, value));
        print(111);
    }
}
