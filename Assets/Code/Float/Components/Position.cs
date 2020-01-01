using System;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct Position : IComponentData
{
	public float3 Value;
}
