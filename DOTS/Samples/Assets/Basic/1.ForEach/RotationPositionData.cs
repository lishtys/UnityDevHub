using Unity.Entities;

namespace Basic._1.Intro
{
   [GenerateAuthoringComponent]
   public struct RotationPositionData : IComponentData
   {
      public float RadiansPerSecond;
   }
}
