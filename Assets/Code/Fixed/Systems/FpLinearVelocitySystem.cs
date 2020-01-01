using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics.FixedPoint;

[UpdateAfter(typeof(LinearForceSystem))]
public class FpLinearVelocitySystem : JobComponentSystem
{
	[BurstCompile]
	struct FpLinearVelocitySystemJob : IJobForEach<FpPosition, FpLinearVelocity>
	{
		public fp timeStep;

		public void Execute(
			ref FpPosition position,
			[ReadOnly] ref FpLinearVelocity velocity)
		{
			position.Value += velocity.Value * timeStep;
		}
	}

	protected override JobHandle OnUpdate(JobHandle inputDependencies)
	{
		var job = new FpLinearVelocitySystemJob();

		job.timeStep = (fp)UnityEngine.Time.fixedDeltaTime;

		return job.Schedule(this, inputDependencies);
	}
}