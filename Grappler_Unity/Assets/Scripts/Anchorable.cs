using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Anchorable : MonoBehaviour {
	public Rigidbody2D rigidbody2D {get; private set;}
	private AnchorPoint[] anchorPoints;

	private void Awake() {
		rigidbody2D = GetComponent<Rigidbody2D>();
		anchorPoints = GetComponentsInChildren<AnchorPoint>();
		if (anchorPoints == null || anchorPoints.Length == 0) Debug.LogError("object has no anchor points!");
	}

	public Vector2 GetRandomLocalAnchorPoint() {
		AnchorPoint anchorPoint = anchorPoints[Random.Range(0, anchorPoints.Length)];
		return anchorPoint.transform.localPosition;
	}
}
