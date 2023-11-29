using System;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class TargetDataStorage : ISaveStorage<int>
    {
        private const string Key = "target_index";
        
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