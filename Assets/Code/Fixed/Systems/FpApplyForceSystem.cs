using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Mathematics.FixedPoint;

[UpdateBefore(typeof(LinearForceSystem))]
public class FpApplyForceSystem : JobComponentSystem
{
	public static fp3 Target { get; set; }

	[BurstCompile]
	struct FpApplyForceSystemJob : IJobForEach<FpLinearForce, FpPosition, FpMass>
	{
		public fp3 target;
		public fp timeStep;
		public fp C1;

		public void Execute(
			ref FpLinearForce force,
			[ReadOnly] ref FpPosition position,
			[ReadOnly] ref FpMass mass)
		{
			force.Value += C1 * fpmath.normalize(target - position.Value) * timeStep / mass.Value;
		}
	}

	protected override JobHandle OnUpdate(JobHandle inputDependencies)
	{
		var job = new FpApplyForceSystemJob();

		job.timeStep = (fp)UnityEngine.Time.fixedDeltaTime;
		job.target = Target;
		job.C1 = 1000;

		return job.Schedule(this, inputDependencies);
	}
}