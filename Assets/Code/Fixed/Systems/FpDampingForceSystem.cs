using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics.FixedPoint;

[UpdateBefore(typeof(ApplyForceSystem))]
public class FpDampingSystem : JobComponentSystem
{
	public static readonly fp C1 = 40m;
	public static readonly fp C2 = 0.4m;
	public static readonly fp C3 = 0.1m;

	[BurstCompile]
	struct FpDampingSystemJob : IJobForEach<FpLinearVelocity, FpLinearForce>
	{
		public fp timeStep;
		//TODO: Figure out how to deal with magic constants in dots

		public void Execute(
			ref FpLinearVelocity velocity,
			ref FpLinearForce force)
		{
			if (fpmath.lengthsq(velocity.Value) > C1)
			{
				force.Value -= velocity.Value * C2;
				force.Value.y += velocity.Value.x * C3 + velocity.Value.z * C3;
			}
		}
	}

	protected override JobHandle OnUpdate(JobHandle inputDependencies)
	{
		var job = new FpDampingSystemJob();

		job.timeStep = (fp)UnityEngine.Time.fixedDeltaTime;

		return job.Schedule(this, inputDependencies);
	}
}