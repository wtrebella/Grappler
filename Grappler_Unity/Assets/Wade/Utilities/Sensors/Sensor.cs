using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sensor<T> : WadeBehaviour where T : WadeBehaviour
{
	List<T> _sensedTs = new List<T>();
	public List<T> GetSensed()
	{ return _sensedTs; }

	public System.Action<T> SensedActorCallback = delegate { };
	public System.Action<T> UnsensedActorCallback = delegate { };

	void OnTriggerEnter(Collider collider)
	{
		if (collider.attachedRigidbody)
		{
			T t = collider.attachedRigidbody.GetComponent<T>();
			if (t && !_sensedTs.Contains(t))
			{
				_sensedTs.Add(t);
				SensedActorCallback(t);
			}
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.attachedRigidbody)
		{
			T t = collider.attachedRigidbody.GetComponent<T>();
			if (t)
			{
				_sensedTs.Remove(t);
				UnsensedActorCallback(t);
			}
		}
	}
}
