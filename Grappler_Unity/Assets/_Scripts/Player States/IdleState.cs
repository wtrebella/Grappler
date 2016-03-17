using UnityEngine;
using System.Collections;

public class IdleState : MonoBehaviour {
	[SerializeField] private Transform body;
	[SerializeField] private Transform feet;

	public void ResetRotation() {
		body.localRotation = Quaternion.identity;
		feet.localRotation = Quaternion.identity;
	}
}
