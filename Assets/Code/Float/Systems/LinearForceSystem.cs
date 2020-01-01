using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

public class LinearForceSystem : JobComponentSystem
{
	[BurstCompile]
	struct LinearForceSystemJob : IJobForEach<LinearVelocity, LinearForce>
	{
		public float timeStep;

		public void Execute(
			ref LinearVelocity velocity,
			ref LinearForce force)
		{
			velocity.Value += force.Value * timeStep;
			force.Value = float3.zero;
		}
	}

	protected override JobHandle OnUpdate(JobHandle inputDependencies)
	{
		var job = new LinearForceSystemJob();

		job.timeStep = UnityEngine.Time.fixedDeltaTime;

		return job.Schedule(this, inputDependencies);
	}
}