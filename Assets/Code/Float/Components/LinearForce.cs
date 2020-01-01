using System;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct LinearForce : IComponentData
{
	public float3 Value;
}
