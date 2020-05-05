using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class EntitySpawnMono : MonoBehaviour
{
   [SerializeField]
   private GameObject prefab;

   private void Start()
   {
      // load
      // Create entity prefab from the game object hierarchy once
      var entityMrg = World.DefaultGameObjectInjectionWorld.EntityManager;
      var setting = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
      var entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, setting);
      
      for (int i = 0; i < 100; i++)
      {
         for (int j = 0; j < 100; j++)
         {
            var instance= entityMrg.Instantiate(entityPrefab);
            var position = transform.TransformPoint(new float3(1, noise.cnoise(new float2(i, j) * .21f),1f));
            entityMrg.AddComponentData(instance, new Translation()
            {
               Value = position
            });
         }
      }
   }
}
