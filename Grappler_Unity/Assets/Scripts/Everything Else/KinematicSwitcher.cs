using UnityEngine;
using System.Collections;

public class KinematicSwitcher : MonoBehaviour {
	[SerializeField] private Rigidbody2D[] rigidbodies;

	public void SetKinematic() {
		foreach (Rigidbody2D r in rigidbodies) r.isKinematic = true;
	}

	public void SetNonKinematic() {
		foreach (Rigidbody2D r in rigidbodies) r.isKinematic = false;
	}
}
