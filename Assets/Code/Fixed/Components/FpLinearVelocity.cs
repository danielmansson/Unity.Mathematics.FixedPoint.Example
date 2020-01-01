using System;
using Unity.Entities;
using Unity.Mathematics.FixedPoint;

[Serializable]
public struct FpLinearVelocity : IComponentData
{
	public fp3 Value;
}
