using System;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    /*
     * Уникальные
     * Run Time Данные
     */
    [Serializable]
    public class GameData
    {
        public DiamondStorage DiamondStorage = new DiamondStorage();
        public TargetPointStorage TargetPointStorage = new TargetPointStorage();
        public TargetDataStorage TargetDataIndexStorage = new TargetDataStorage();
        
        public float Speed;
    }
}