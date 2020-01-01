using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Mathematics.FixedPoint;
using UnityEngine;

public class FpSpawnerAuthoring : SimpleAuthoring<FpSpawner>, IDeclareReferencedPrefabs
{
	public GameObject prefab;
	public float3 center;
	public float radius;
	public float2 massRange;

	public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
	{
		referencedPrefabs.Add(prefab);
	}

	public override void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
	{
		//TODO: Add editor support for fp
		//Lazy workaround:
		Component.Center.x = (fp)center.x;
		Component.Center.y = (fp)center.y;
		Component.Center.z = (fp)center.z;
		Component.MassRange.x = (fp)massRange.x;
		Component.MassRange.y = (fp)massRange.y;
		Component.Radius = (fp)radius;

		FpApplyForceSystem.Target = Component.Center;

		Component.BodyPrefabEntity = conversionSystem.GetPrimaryEntity(prefab);

		dstManager.AddComponentData(entity, Component);
	}
}
