using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class CenterOfMassSetter : MonoBehaviour {
	[SerializeField] private bool showDebugPoint = false;
	[SerializeField] private Vector2 centerOfMass;
	private Rigidbody2D rigid;

	private void Awake() {
		rigid = GetComponent<Rigidbody2D>();
		rigid.centerOfMass = centerOfMass;
	}

	private void OnDrawGizmos() {
		if (!showDebugPoint) return;
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.TransformPoint((Vector3)centerOfMass), 0.02f);
	}
}
