using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

[UpdateBefore(typeof(LinearForceSystem))]
public class ApplyForceSystem : JobComponentSystem
{
	public static float3 Target { get; set; }

	[BurstCompile]
	struct ApplyForceSystemJob : IJobForEach<LinearForce, Position, Mass>
	{
		public float3 target;
		public float timeStep;

		public void Execute(
			ref LinearForce force,
			[ReadOnly] ref Position position,
			[ReadOnly] ref Mass mass)
		{
			force.Value += 1000 * math.normalize((target - position.Value)) * timeStep / mass.Value;
		}
	}

	protected override JobHandle OnUpdate(JobHandle inputDependencies)
	{
		var job = new ApplyForceSystemJob();

		job.timeStep = UnityEngine.Time.fixedDeltaTime;
		job.target = Target;

		return job.Schedule(this, inputDependencies);
	}
}