using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Mathematics.FixedPoint;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public class FpSpawnerSystem : JobComponentSystem
{
	BeginInitializationEntityCommandBufferSystem entityCommandBufferSystem;

	[BurstCompile]
	struct FpSpawnerSystemJob : IJobForEachWithEntity<FpSpawner>
	{
		public EntityCommandBuffer.Concurrent commandBuffer;

		public void Execute(Entity spawnerEntity, int index, ref FpSpawner spawner)
		{
			var random = new Random(1661);

			for (int i = 0; i < spawner.NumBodies; i++)
			{
				var spawnedEntity = commandBuffer.Instantiate(index, spawner.BodyPrefabEntity);

				var c = spawner.Center + random.NextFp3Direction() * random.NextFp(spawner.Radius);
				//TODO: Write random extensions for fp
				commandBuffer.SetComponent(index, spawnedEntity, new FpPosition()
				{
					Value = c
				});
				commandBuffer.SetComponent(index, spawnedEntity, new FpMass()
				{
					Value = random.NextFp(spawner.MassRange.x, spawner.MassRange.y)
				});
				commandBuffer.SetComponent(index, spawnedEntity, new FpLinearVelocity()
				{
					Value = random.NextFp3Direction()
				});
			}

			commandBuffer.DestroyEntity(index, spawnerEntity);
		}
	}

	protected override void OnCreate()
	{
		entityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
	}

	protected override JobHandle OnUpdate(JobHandle inputDependencies)
	{
		var job = new FpSpawnerSystemJob();

		job.commandBuffer = entityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
		
		var jobHandle = job.Schedule(this, inputDependencies);

		entityCommandBufferSystem.AddJobHandleForProducer(jobHandle);

		return jobHandle;
	}
}

//TODO: Make real deterministic implementation
public static class UnityRandomExtensions
{
	// Danger, danger!
	public static fp3 NextFp3Direction(ref this Random random)
	{
		var r = random.NextFloat3Direction();
		return new fp3((fp)r.x, (fp)r.y, (fp)r.z);
	}

	// Danger, danger!
	public static fp NextFp(ref this Random random, fp max)
	{
		return (fp)random.NextFloat((float)max);
	}

	// Danger, danger!
	public static fp NextFp(ref this Random random, fp min, fp max)
	{
		return (fp)random.NextFloat((float)min, (float)max);
	}
}