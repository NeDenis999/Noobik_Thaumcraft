using UnityEngine;
using Voody.UniLeo;

namespace Noobik_Thaumcraft
{
    [RequireComponent(typeof(EntityReference))]
    public class InitializeEntityProvider : MonoProvider<InitializeEntityRequest> { }
}