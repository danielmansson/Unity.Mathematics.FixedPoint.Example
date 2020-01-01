using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics.FixedPoint;

public class FpLinearForceSystem : JobComponentSystem
{
	[BurstCompile]
	struct FpLinearForceSystemJob : IJobForEach<FpLinearVelocity, FpLinearForce>
	{
		public fp timeStep;

		public void Execute(
			ref FpLinearVelocity velocity,
			ref FpLinearForce force)
		{
			velocity.Value += force.Value * timeStep;
			force.Value = fp3.zero;
		}
	}

	protected override JobHandle OnUpdate(JobHandle inputDependencies)
	{
		var job = new FpLinearForceSystemJob();

		job.timeStep = (fp)UnityEngine.Time.fixedDeltaTime;

		return job.Schedule(this, inputDependencies);
	}
}