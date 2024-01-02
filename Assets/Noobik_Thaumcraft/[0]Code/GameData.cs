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
        public Transform TargetPoint;
        public int IndexConfigTarget;
        
        public int Diamonds;
        public float Speed = 6;
        public int MaxItems = 16;
        public int MiningBoost = 0;
    }
}