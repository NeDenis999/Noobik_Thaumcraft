using System;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class TargetPointStorage : ISaveStorage<Transform>
    {
        public event Action<Transform> Upgrade;

        private Transform _target;
        
        public Transform Get() =>
            _target;

        public void Set(Transform value) =>
            _target = value;

        public void Add(Transform value)
        {
            throw new NotImplementedException();
        }
    }
}