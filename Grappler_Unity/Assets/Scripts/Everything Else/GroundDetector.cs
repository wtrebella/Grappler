using UnityEngine;
using System.Collections;

public class GroundDetector : MonoBehaviour {
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private float maxDistance = 0.5f;

	public bool IsCloseToGround() {
		RaycastHit2D hit = Physics2D.Raycast(rigid.transform.position, new Vector2(1, -1).normalized, maxDistance, 1 << LayerMask.NameToLayer("Ground"));
		if (hit.collider) return true;
		else return false;
	}

}
