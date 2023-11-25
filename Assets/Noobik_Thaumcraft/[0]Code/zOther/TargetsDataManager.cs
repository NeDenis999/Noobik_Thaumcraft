using System;
using System.Collections.Generic;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class TargetsDataManager : MonoBehaviour
    {
        public event Action<TargetData> TargetUpdated;
        
        [SerializeField]
        private List<TargetData> _targetData;

        public IEnumerable<TargetData> TargetData => _targetData;

        public TargetData GetCurrentTarget()
        {
            return _targetData[0];
        }
    }
}