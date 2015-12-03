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
		Gizmos.color = Color.black;
		Gizmos.DrawSphere(transform.TransformPoint(centerOfMass.ToVector3()), 0.05f);
	}
}
