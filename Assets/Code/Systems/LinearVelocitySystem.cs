using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

[UpdateAfter(typeof(LinearForceSystem))]
public class LinearVelocitySystem : JobComponentSystem
{
	[BurstCompile]
	struct LinearVelocitySystemJob : IJobForEach<Position, LinearVelocity>
	{
		public float timeStep;

		public void Execute(
			ref Position position,
			[ReadOnly] ref LinearVelocity velocity)
		{
			position.Value += velocity.Value * timeStep;
		}
	}

	protected override JobHandle OnUpdate(JobHandle inputDependencies)
	{
		var job = new LinearVelocitySystemJob();

		job.timeStep = UnityEngine.Time.fixedDeltaTime;

		return job.Schedule(this, inputDependencies);
	}
}