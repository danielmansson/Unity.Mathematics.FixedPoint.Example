# Unity.Mathematics.FixedPoint.Example

A simple example project for [Unity.Mathematics.FixedPoint](https://github.com/danielmansson/Unity.Mathematics.FixedPoint).

The project contains two side-by-side implementation of the same simulation, bodies with independant gravity towards a point, one using floating point math and the other using fixed point math. 

This is not meant to be an extensive performance test, but can give an indication how the fixed point implementation compares to floating point. It can also be used to get an indication if Burst has any effect on the fixed point math.

My first observations on a single machine, in-editor, profiling the heaviest system:
- Without Burst, floating point is \~10x faster than fixed point.
- With Burst, floating point gains \~12x performance.
- With Burst, fixed point point gains \~9x performance.

## Known issues

[Unity.Mathematics.FixedPoint](https://github.com/danielmansson/Unity.Mathematics.FixedPoint) is currently not complete. There are API issues which needs fixing and this example exposed a few of those issues, especially in combination with Unity's ECS and Burst.

Keep an eye on the active issues in [Unity.Mathematics.FixedPoint Issues](https://github.com/danielmansson/Unity.Mathematics.FixedPoint/issues).
