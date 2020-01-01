using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics.FixedPoint;

[UpdateBefore(typeof(ApplyForceSystem))]
public class FpDampingSystem : JobComponentSystem
{
	[BurstCompile]
	struct FpDampingSystemJob : IJobForEach<FpLinearVelocity, FpLinearForce>
	{
		public fp timeStep;
		public fp C1;
		public fp C2;
		public fp C3;
		//TODO: Figure out how to deal with magic constants in dots

		public void Execute(
			ref FpLinearVelocity velocity,
			ref FpLinearForce force)
		{
			if (fpmath.lengthsq(velocity.Value) > C1)
			{
				force.Value -= velocity.Value * C2;
				force.Value.y += velocity.Value.x * C3 + velocity.Value.z * C3;
			}
		}
	}

	protected override JobHandle OnUpdate(JobHandle inputDependencies)
	{
		var job = new FpDampingSystemJob();

		job.timeStep = (fp)UnityEngine.Time.fixedDeltaTime;
		job.C1 = 40m;
		job.C2 = 0.4m;
		job.C3 = 0.1m;

		return job.Schedule(this, inputDependencies);
	}
}