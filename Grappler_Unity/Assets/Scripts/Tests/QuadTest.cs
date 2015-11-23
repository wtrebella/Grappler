using UnityEngine;
using System.Collections;

public class QuadTest : MonoBehaviour {
	public Quad quad;

	public Vector2 testPoint = new Vector2(5, 5);
	
	void OnDrawGizmos() {

	}

	public void Reset() {
		quad.bottomLeft = Vector2.zero;
		quad.topLeft = new Vector2(0, 10);
		quad.topRight = new Vector2(10, 10);
		quad.bottomRight = new Vector2(10, 0);

		testPoint = new Vector2(5, 5);
	}
}
