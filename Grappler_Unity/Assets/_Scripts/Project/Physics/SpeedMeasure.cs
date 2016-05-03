using UnityEngine;
using System.Collections;

public class SpeedMeasure : MonoBehaviour {
	[SerializeField] private Rigidbody2D rigid;

	public float GetSpeed() {
		return rigid.velocity.magnitude;
	}

	public float GetXSpeed() {
		return rigid.velocity.x;
	}

	public float GetYSpeed() {
		return rigid.velocity.y;
	}
}
