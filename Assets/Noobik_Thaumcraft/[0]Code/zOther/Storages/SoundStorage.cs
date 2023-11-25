﻿using System;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class SoundStorage : ISaveStorage<bool>
    {
        private const string Key = "IsSound";
        
        public event Action<bool> Upgrade;
        
        public bool Get() =>
            PlayerPrefs.GetInt(Key, 1) == 1;

        public void Set(bool value) =>
            PlayerPrefs.SetInt(Key, value ? 1 : 0);

        public void Add(bool value)
        {
            throw new NotImplementedException();
        }
    }
}