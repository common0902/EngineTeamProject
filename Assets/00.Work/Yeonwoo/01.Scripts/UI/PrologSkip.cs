using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologSkip : MonoBehaviour
{
   public void Skip()
   {
      SceneManager.LoadScene(2);
   }
}
