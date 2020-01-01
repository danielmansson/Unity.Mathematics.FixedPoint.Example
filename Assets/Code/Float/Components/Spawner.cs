using System;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct Spawner : IComponentData
{
	public Entity BodyPrefabEntity;
	public int NumBodies;
	public float3 Center;
	public float Radius;
	public float2 MassRange;
}
