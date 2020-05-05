using Unity.Entities;
using UnityEngine;

namespace Basic._4.SpawnFromEntity
{
    /// <summary>
    ///  Used by Spawner System
    /// </summary>
    public struct SpawnerData:IComponentData
    {
        public int CountX;
        public int CountY;
        public Entity Prefab;
    }
}