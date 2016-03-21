using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KinematicSwitcher : MonoBehaviour {
	private List<Rigidbody2D> rigidbodies;

	private void Awake() {
		rigidbodies = new List<Rigidbody2D>();
	}

	public void AddRigidbody(Rigidbody2D rigid) {
		rigidbodies.Add(rigid);
	}

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
