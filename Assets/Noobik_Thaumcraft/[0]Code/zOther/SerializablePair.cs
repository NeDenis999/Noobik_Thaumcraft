using System;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    [Serializable]
    public struct SerializablePair<TKey, TValue>
    {
        [field: SerializeField] public TKey Key { get; private set; }
        [field: SerializeField] public TValue Value { get; private set; }
    }
}