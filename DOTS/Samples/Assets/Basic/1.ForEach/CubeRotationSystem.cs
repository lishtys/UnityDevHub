using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Basic._1.Intro
{
    public class CubeRotationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            Entities
                .WithName("RotationPositionData")
                .ForEach((ref Rotation rotation, in RotationPositionData rSpeed) =>
                {
                    rotation.Value = math.mul(
                        math.normalize(rotation.Value), quaternion.AxisAngle(math.up(), rSpeed.RadiansPerSecond * deltaTime)
                    );
                }).ScheduleParallel();
        }
    }
}
