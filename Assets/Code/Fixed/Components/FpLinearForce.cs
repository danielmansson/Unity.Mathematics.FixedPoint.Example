using System;
using Unity.Entities;
using Unity.Mathematics.FixedPoint;

[Serializable]
public struct FpLinearForce : IComponentData
{
	public fp3 Value;
}
