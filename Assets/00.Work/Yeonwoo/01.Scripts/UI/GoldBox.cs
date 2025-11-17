using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public class GoldBox : AbstractBox
    {
        protected override void BoxOpenAnim() // 이건 뭐지지지지지지직
        {
            base.BoxOpenAnim();
            Debug.Log("황금 방 박스 오픈");
        }
    }
}