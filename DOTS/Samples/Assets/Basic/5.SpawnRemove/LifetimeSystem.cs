using Unity.Entities;

namespace Basic._5.SpawnRemove
{
   [GenerateAuthoringComponent]
   public struct LifetimeData : IComponentData
   {
      public float LifeTime;
   }


   public class LifeTimeSystem : SystemBase
   {
      private EntityCommandBufferSystem _commandBufferSystem;

      protected override void OnCreate()
      {
         _commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
      }

      protected override void OnUpdate()
      {
         var commandBuffer = _commandBufferSystem.CreateCommandBuffer().ToConcurrent();
         var deltaTime = Time.DeltaTime;
         Entities.ForEach((Entity entity, int nativeThreadIndex, ref LifetimeData lifetimeData) =>
            {
               lifetimeData.LifeTime -= deltaTime;
               if (lifetimeData.LifeTime <= 0)
               {
                  commandBuffer.DestroyEntity(nativeThreadIndex,entity);
               }
            }).ScheduleParallel();
         
         _commandBufferSystem.AddJobHandleForProducer(Dependency);
      }
   }
}


