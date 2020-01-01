using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateAfter(typeof(LinearVelocitySystem))]
public class FpUpdateTranslationSystem : JobComponentSystem
{
	[BurstCompile]
	struct FpUpdateTranslationSystemJob : IJobForEach<Translation, FpPosition>
	{
		public float timeStep;

		public void Execute(
			ref Translation translation,
			[ReadOnly] ref FpPosition position)
		{
			//TODO: Explicit conversion
			translation.Value = new float3((float)position.Value.x, (float)position.Value.y, (float)position.Value.z);
		}
	}

	protected override JobHandle OnUpdate(JobHandle inputDependencies)
	{
		var job = new FpUpdateTranslationSystemJob();

		job.timeStep = UnityEngine.Time.fixedDeltaTime;

		return job.Schedule(this, inputDependencies);
	}
}