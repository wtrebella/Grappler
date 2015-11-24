using UnityEngine;
using System.Collections;

public class PointInQuadTest : MonoBehaviour {
	public Quad quad;

	public Vector2 testPoint = new Vector2(5, 5);

	public void Reset() {
		if (quad == null) quad = new Quad(Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero);

		quad.bottomLeft = Vector2.zero;
		quad.topLeft = new Vector2(0, 10);
		quad.topRight = new Vector2(10, 10);
		quad.bottomRight = new Vector2(10, 0);

		testPoint = new Vector2(5, 5);
	}
}
