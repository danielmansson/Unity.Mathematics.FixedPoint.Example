using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Mathematics.FixedPoint;

[Serializable]
public struct FpSpawner : IComponentData
{
	public Entity BodyPrefabEntity;
	public int NumBodies;
	public fp3 Center;
	public fp Radius;
	public fp2 MassRange;
}
