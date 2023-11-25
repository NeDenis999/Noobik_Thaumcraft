using UnityEngine;

namespace Noobik_Thaumcraft
{
    [CreateAssetMenu(fileName = "Target", order = 81, menuName = "Data/Target")]
    public class TargetData : ScriptableObject
    {
        public Sprite Icon;
        public int Amount;
        public Location Location;
    }
}