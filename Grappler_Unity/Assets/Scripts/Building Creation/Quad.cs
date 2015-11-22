using UnityEngine;
using System.Collections;

public class Quad {
	public Vector2 bottomLeft = Vector2.zero;
	public Vector2 topLeft = Vector2.zero;
	public Vector2 bottomRight = Vector2.zero;
	public Vector2 topRight = Vector2.zero;

	public float bottomWidth {
		get {return bottomRight.x - bottomLeft.x;}
	}

	public float averageHeight {
		get {return (topLeft.y + topRight.y) / 2f;}
	}

	private bool boundsAreSet = false;

	private Bounds _bounds;
	public Bounds bounds {
		get {
			if (!boundsAreSet) CalculateBounds();
			return _bounds;
		}
	}

	public Quad() {

	}

	public Quad(Vector2 bottomLeft, Vector2 topLeft, Vector2 bottomRight, Vector2 topRight) {
		this.bottomLeft = bottomLeft;
		this.topLeft = topLeft;
		this.bottomRight = bottomRight;
		this.topRight = topRight;

		CalculateBounds();
	}

	private void CalculateBounds() {
		Rect containingRect = GetContainingRect();
		_bounds = new Bounds(containingRect.center, containingRect.size);
		boundsAreSet = true;
	}

	private Rect GetContainingRect() {
		Vector2 origin = new Vector2(Mathf.Min(bottomLeft.x, topLeft.x), Mathf.Min(bottomLeft.y, bottomRight.y));
		Vector2 maxCorner = new Vector2(Mathf.Max(bottomRight.x, topRight.x), Mathf.Max(topLeft.y, topRight.y));
		Vector2 size = new Vector2(maxCorner.x - origin.x, maxCorner.y - origin.y);

		Rect containingRect = new Rect(origin, size);
		return containingRect;
	}
}
