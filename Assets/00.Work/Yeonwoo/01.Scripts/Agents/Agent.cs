using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Agents
{
    public abstract class Agent : MonoBehaviour
    {
        private Dictionary<Type, IComponent> _componentDict = new Dictionary<Type, IComponent>();

        protected virtual void Awake()
        {
            _componentDict = GetComponentsInChildren<IComponent>()
                .ToDictionary(compo => compo.GetType());
        }
    }
}