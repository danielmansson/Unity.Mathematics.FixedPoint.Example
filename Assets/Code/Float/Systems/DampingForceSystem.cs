using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

[UpdateBefore(typeof(ApplyForceSystem))]
public class DampingSystem : JobComponentSystem
{
	[BurstCompile]
	struct DampingSystemJob : IJobForEach<LinearVelocity, LinearForce>
	{
		public float timeStep;

		public void Execute(
			ref LinearVelocity velocity,
			ref LinearForce force)
		{
			if (math.lengthsq(velocity.Value) > 40f)
			{
				force.Value -= velocity.Value * 0.4f;
				force.Value.y += velocity.Value.x * 0.01f + velocity.Value.z * 0.01f;
			}
		}
	}

	protected override JobHandle OnUpdate(JobHandle inputDependencies)
	{
		var job = new DampingSystemJob();

		job.timeStep = UnityEngine.Time.fixedDeltaTime;

		return job.Schedule(this, inputDependencies);
	}
}