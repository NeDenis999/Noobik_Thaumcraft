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
    }
}