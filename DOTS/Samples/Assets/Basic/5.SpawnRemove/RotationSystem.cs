using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Basic._5.SpawnRemove
{
    public class RotationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            Entities.WithName("RotationSystem").
                ForEach((ref Rotation rotation, in RotationSpeedData rotationSpeedData) =>
                {
                    var radian = math.radians(rotationSpeedData.Degree);
                    rotation.Value = math.mul(math.normalize(rotation.Value),
                        quaternion.AxisAngle(math.up(), radian * deltaTime));
                }).ScheduleParallel();
        }
    }
}
