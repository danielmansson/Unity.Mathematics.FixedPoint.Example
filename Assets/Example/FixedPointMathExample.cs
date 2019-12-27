using System;
using Unity.Mathematics.FixedPoint;
using UnityEngine;

public class FixedPointMathExample : MonoBehaviour
{
	public fp value = fp.one;
	public fp2 value2 = fpmath.fp2(fp.one, fp.one);

	public string a = "7";
	public string b = "4";

	private void OnGUI()
	{
		a = GUILayout.TextField(a);
		b = GUILayout.TextField(b);

		try
		{
			fp v1 = decimal.Parse(a);
			fp v2 = decimal.Parse(b);

			var result = v1 + v2;
			GUILayout.Label("Result: " + result.ToString());
			GUILayout.Label("Sqrt(a): " + fpmath.sqrt(v1).ToString());
			GUILayout.Label("Raw Value: " + value.ToString());
			GUILayout.Label("Raw Value 2: " + value2.ToString());
		}
		catch (Exception)
		{
			GUILayout.Label("INV");
		}
	}
}
