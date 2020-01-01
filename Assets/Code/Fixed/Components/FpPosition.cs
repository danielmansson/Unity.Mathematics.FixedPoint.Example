using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Mathematics.FixedPoint;

[Serializable]
public struct FpPosition : IComponentData
{
	public fp3 Value;
}
