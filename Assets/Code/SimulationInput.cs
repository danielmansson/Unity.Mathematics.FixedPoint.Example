using Unity.Mathematics.FixedPoint;
using UnityEngine;

public class SimulationInput : MonoBehaviour
{
	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out var hit))
			{
				ApplyForceSystem.Target = hit.point;
			}
		}

		if (Input.GetMouseButton(1))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out var hit))
			{
				//TODO Explicit conversions
				FpApplyForceSystem.Target = new fp3((fp)hit.point.x, (fp)hit.point.y, (fp)hit.point.z);
			}
		}
	}
}
