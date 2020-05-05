using Unity.Entities;

namespace Basic._5.SpawnRemove
{
    [GenerateAuthoringComponent]
    public struct SpawnData : IComponentData
    {
        public int CountX;
        public int CountY;
        public Entity Prefab;
    }
}