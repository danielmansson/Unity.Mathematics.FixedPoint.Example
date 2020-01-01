using System;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct LinearVelocity : IComponentData
{
	public float3 Value;
}
