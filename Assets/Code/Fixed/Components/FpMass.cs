using System;
using Unity.Entities;
using Unity.Mathematics.FixedPoint;

[Serializable]
public struct FpMass : IComponentData
{
	public fp Value;
}
