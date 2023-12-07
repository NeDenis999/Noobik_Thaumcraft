using System.Collections.Generic;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    /*
     * Ссылки на ресурсы
     * глобальные данные
     */
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public List<TargetData> TargetData;
        public float DistanceCell = 0.5f;
        public float MagnetizationSpeed = 6;
        public float PickUpDistance = 0.5f;
        public double DropDistance = 0.5f;
        public float NotDropTime = 0.5f;
        public float SizeBlockInContainer = 1.3f;
    }
}