using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpawnerAuthoring : SimpleAuthoring<Spawner>, IDeclareReferencedPrefabs
{
	public GameObject prefab;

	public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
	{
		referencedPrefabs.Add(prefab);
	}

	public override void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
	{
		ApplyForceSystem.Target = Component.Center;

		Component.BodyPrefabEntity = conversionSystem.GetPrimaryEntity(prefab);

		dstManager.AddComponentData(entity, Component);
	}
}
