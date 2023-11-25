using System;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class DiamondStorage : ISaveStorage<int>
    {
        private const string Key = "Diamond";
        
        public event Action<int> Upgrade;
        
        public int Get() =>
            PlayerPrefs.GetInt(Key);

        public void Set(int value)
        {
            PlayerPrefs.SetInt(Key, value);
            Upgrade?.Invoke(value);
        }

        public void Add(int value)
        {
            Set(Get() + value);
        }
    }
}