using Unity.Entities;
using UnityEngine;

[DisallowMultipleComponent]
[RequiresEntityConversion]
public abstract class SimpleAuthoring<T> : MonoBehaviour, IConvertGameObjectToEntity where T : struct, IComponentData
{
	public T Component;

	public virtual void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
	{
		dstManager.AddComponentData(entity, Component);
	}
}
