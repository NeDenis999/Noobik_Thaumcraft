using System.Collections.Generic;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class TargetPointsContainer : MonoBehaviour
    {
        [SerializeField]
        private List<SerializablePair<Location, List<Transform>>> _targetPoints;

        public Transform GetPoint(Location _location, Transform heroTransform)
        {
            foreach (var point in _targetPoints)
            {
                if (point.Key == _location)
                {
                    float minDistance = float.MaxValue;
                    Transform result = point.Value[0];
                    
                    foreach (var target in point.Value)
                    {
                        var distance = Vector3.Distance(target.position, heroTransform.position);

                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            result = target;
                        }
                    }
                    
                    return result;
                }
            }

            return null;
        }
    }
}