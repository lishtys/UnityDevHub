using System.ComponentModel.Design.Serialization;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Basic._4.SpawnFromEntity
{
    
    public class EntitySpawnInSystem:SystemBase
    {
        private BeginInitializationEntityCommandBufferSystem _commandBufferSystem;
        protected override void OnCreate()
        {
            _commandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
            Debug.Log("--On Create-----!");

        }


        protected override void OnUpdate()
        {
            var commandBuffer = _commandBufferSystem.CreateCommandBuffer().ToConcurrent();

            Entities
                .WithName("Spawner")
                .WithBurst(FloatMode.Fast, FloatPrecision.Standard,
                    true) //SyncCompilation: ensure Burst compiles it before running to get the best performance
                .ForEach((Entity entity, int  entityInQueryIndex,in SpawnerData spawnerData,in LocalToWorld localToWorld ) =>  //??
                {

                    for (int i = 0; i < spawnerData.CountX; i++)
                    {
                        for (int j = 0; j < spawnerData.CountY; j++)
                        {
                            // Instantiate commands to the EntityCommandBuffer.
                            var instance= commandBuffer.Instantiate(entityInQueryIndex, spawnerData.Prefab);
                    
                            var position = math.transform(localToWorld.Value,
                                new float3(i * 1.3F, noise.cnoise(new float2(i, j) * 0.21F) * 2, j * 1.3F));
                            commandBuffer.SetComponent(entityInQueryIndex, instance, new Translation {Value = position});

                        }
                        
                    }
                    commandBuffer.DestroyEntity(entityInQueryIndex, entity);
                
                }).ScheduleParallel();
            
            
            
            _commandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }
}