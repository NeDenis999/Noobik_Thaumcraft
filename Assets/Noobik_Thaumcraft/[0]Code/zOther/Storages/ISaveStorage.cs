using System;


namespace Noobik_Thaumcraft
{
    public interface ISaveStorage<T>
    {
        public event Action<T> Upgrade;
        public T Get();
        public void Set(T value);
        public void Add(T value);
    }
}