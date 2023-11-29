using UnityEngine;
using Voody.UniLeo;

namespace Noobik_Thaumcraft
{
    [RequireComponent(typeof(EntityBehaviour))]
    public class InitializeEntityProvider : MonoProvider<InitializeEntityRequest> { }
}