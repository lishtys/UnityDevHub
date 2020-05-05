using System.ComponentModel.Design;
using Unity.Collections;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Basic._2.JobChunk
{
    public class RotationJobChunkSystem : SystemBase
    {
        private EntityQuery _query;
        
        protected override void OnCreate()
        {
            _query = GetEntityQuery(typeof(Rotation),ComponentType.ReadOnly<RotationSpeedJobChunkData>());
        }
        
        //runs on main threads
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            var rotationType = GetArchetypeChunkComponentType<Rotation>();
            var rotationSpeedType = GetArchetypeChunkComponentType<RotationSpeedJobChunkData>(true); // it is readonly
            
            // do the job
            var job = new RotationJob()
            {
                rType = rotationType,
                rotationSpeedType = rotationSpeedType,
                deltaTime = deltaTime
            };
            
            Dependency= job.Schedule(_query, Dependency);
        }

        
        struct RotationJob : IJobChunk
        {
            public ArchetypeChunkComponentType<Rotation> rType;
            [ReadOnly]
            public ArchetypeChunkComponentType<RotationSpeedJobChunkData> rotationSpeedType;
            public float deltaTime;
            
            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                var rotationComponent = chunk.GetNativeArray(rType);
                var speedComponent = chunk.GetNativeArray(rotationSpeedType);

                for (int i = 0; i < chunk.Count; i++)
                {
                      var rotation = rotationComponent[i];
                      var rotationSpeed = speedComponent[i];

                      rotationComponent[i] = new Rotation()
                      {
                          Value = math.mul(math.normalize(rotation.Value),
                              quaternion.AxisAngle(math.up(), rotationSpeed.Radian * deltaTime))
                      };
                }
               
            }
        }
    }
}