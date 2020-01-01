using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public class SpawnerSystem : JobComponentSystem
{
	BeginInitializationEntityCommandBufferSystem entityCommandBufferSystem;

	struct SpawnerSystemJob : IJobForEachWithEntity<Spawner>
	{
		public EntityCommandBuffer.Concurrent commandBuffer;

		public void Execute(Entity spawnerEntity, int index, ref Spawner spawner)
		{
			var random = new Random(1661);

			for (int i = 0; i < spawner.NumBodies; i++)
			{
				var spawnedEntity = commandBuffer.Instantiate(index, spawner.BodyPrefabEntity);

				commandBuffer.SetComponent(index, spawnedEntity, new Position()
				{
					Value = spawner.Center + random.NextFloat3Direction() * random.NextFloat(spawner.Radius)
				});
				commandBuffer.SetComponent(index, spawnedEntity, new Mass()
				{
					Value = random.NextFloat(spawner.MassRange.x, spawner.MassRange.y)
				});
				commandBuffer.SetComponent(index, spawnedEntity, new LinearVelocity()
				{
					Value = random.NextFloat3Direction()
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
		var job = new SpawnerSystemJob();

		job.commandBuffer = entityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
		
		var jobHandle = job.Schedule(this, inputDependencies);

		entityCommandBufferSystem.AddJobHandleForProducer(jobHandle);

		return jobHandle;
	}
}