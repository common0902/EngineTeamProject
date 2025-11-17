using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class DefaultBox : AbstractBox
    {
        protected override void BoxOpenAnim()
        {
            base.BoxOpenAnim();
            Debug.Log("전투 승리 박스 오픈");
        }
    }
}