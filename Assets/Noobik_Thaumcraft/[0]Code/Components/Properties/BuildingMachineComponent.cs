using System;
using System.Collections.Generic;

namespace Noobik_Thaumcraft
{
    [Serializable]
    public struct BuildingMachineComponent
    {
        public List<SerializablePair<ResourceType, int>> CreateResources;
        public List<SerializablePair<ResourceType, int>> AddedResources;
    }
}