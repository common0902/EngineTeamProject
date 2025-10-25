using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
   public class PrologSkip : MonoBehaviour
   {
      public void Skip()
      {
         SceneManager.LoadScene("Title");
      }
   }
}
