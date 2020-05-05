using System.Collections;
using System.Collections.Generic;
using Basic._4.SpawnFromEntity;
using Unity.Entities;
using UnityEngine;

public class SpawerProxy : MonoBehaviour,IConvertGameObjectToEntity,IDeclareReferencedPrefabs
{
    public GameObject prefab;
    public int CountX;
    public int CountY;
    

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
       var compData= new SpawnerData()
        {
            CountX = CountX,
            CountY = CountY,
            //Simply map gameobject with entity
            Prefab = conversionSystem.GetPrimaryEntity(prefab)
        };
       dstManager.AddComponentData(entity, compData);
    }

    /// <summary>
    ///  Referenced prefabs have to be declared so that the conversion system knows about them ahead of time
    /// </summary>
    /// <param name="referencedPrefabs"></param>
    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(prefab);
    }
}
