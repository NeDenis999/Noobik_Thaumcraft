using System.Collections.Generic;
using UnityEngine;

namespace Noobik_Thaumcraft
{
    /*
     * Уникальные данные сцены
     * Выставляются вручную
     */
    public class SceneData : MonoBehaviour
    {
        public List<SerializablePair<Location, List<Transform>>> TargetPoints;
        public Joystick Joystick;
        public UI UI;
    }
}