using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KinematicSwitcher : MonoBehaviour {
	[SerializeField] private List<Rigidbody2D> rigidbodies;

	public void SetKinematic() {
		foreach (Rigidbody2D r in rigidbodies) r.isKinematic = true;
	}

	public void SetNonKinematic() {
		foreach (Rigidbody2D r in rigidbodies) r.isKinematic = false;
	}

	private void OnDestroy() {
		rigidbodies.Clear();
	}
}
