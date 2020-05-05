using System.Collections;
using System.Collections.Generic;
using Basic._2.JobChunk;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[RequiresEntityConversion]
public class RotationSpeedProxy : MonoBehaviour,IConvertGameObjectToEntity
{
    public float angleInDegree;
    
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        
        var rotationSpeedInRadianData=new RotationSpeedJobChunkData()
        {
            Radian = math.radians(angleInDegree)
        };
        dstManager.AddComponentData(entity, rotationSpeedInRadianData);
    }
}
