using UnityEngine;
using System.Collections;

public class QuadOverlapTest : MonoBehaviour {
	public Quad quad1;
	public Quad quad2;

	public void Reset() {
		if (quad1 == null) quad1 = new Quad(Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero);
		if (quad2 == null) quad2 = new Quad(Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero);

		quad1.bottomLeft = Vector2.zero;
		quad1.topLeft = new Vector2(0, 10);
		quad1.topRight = new Vector2(10, 10);
		quad1.bottomRight = new Vector2(10, 0);

		quad2.bottomLeft = new Vector2(20, 0);
		quad2.topLeft = new Vector2(20, 10);
		quad2.topRight = new Vector2(30, 10);
		quad2.bottomRight = new Vector2(30, 0);
	}
}
