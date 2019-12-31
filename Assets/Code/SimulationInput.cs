using UnityEngine;

public class SimulationInput : MonoBehaviour
{
	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if(Physics.Raycast(ray, out var hit))
			{
				ApplyForceSystem.Target = hit.point;
			}
		}
	}
}
