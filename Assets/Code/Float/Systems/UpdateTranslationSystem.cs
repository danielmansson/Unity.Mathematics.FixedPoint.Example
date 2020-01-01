using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

[UpdateAfter(typeof(LinearVelocitySystem))]
public class UpdateTranslationSystem : JobComponentSystem
{
	[BurstCompile]
	struct UpdateTranslationSystemJob : IJobForEach<Translation, Position>
	{
		public float timeStep;

		public void Execute(
			ref Translation translation,
			[ReadOnly] ref Position position)
		{
			translation.Value = position.Value;
		}
	}

	protected override JobHandle OnUpdate(JobHandle inputDependencies)
	{
		var job = new UpdateTranslationSystemJob();

		job.timeStep = UnityEngine.Time.fixedDeltaTime;

		return job.Schedule(this, inputDependencies);
	}
}