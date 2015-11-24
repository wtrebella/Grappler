using UnityEngine;
using System.Collections;

[System.Serializable]
public class Quad {
	public static bool ContainsPoint(Quad quad, Vector2 point) {
		int i, j;
		Vector2[] corners = new Vector2[] {
			quad.bottomLeft,
			quad.topLeft,
			quad.topRight,
			quad.bottomRight
		};

		bool result = false;
		for (i = 0, j = corners.Length - 1; i < corners.Length; j = i++) {
			if ((corners[i].y > point.y) != (corners[j].y > point.y) &&
			    (point.x < (corners[j].x - corners[i].x) * (point.y - corners[i].y) / (corners[j].y - corners[i].y) + corners[i].x)) {
				result = !result;
			}
		}
		return result;
	}
	
	public static bool ContainsPoint(Vector2 bottomLeft, Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 point) {
		return ContainsPoint(new Quad(bottomLeft, topLeft, topRight, bottomRight), point);
	}

	public static bool OverlapQuads(Quad quad1, Quad quad2) {
		bool triIntersect1 = TriTriIntersection.TriTriOverlap.TriTriIntersect(quad1.GetTri1(), quad2.GetTri1());
		bool triIntersect2 = TriTriIntersection.TriTriOverlap.TriTriIntersect(quad1.GetTri1(), quad2.GetTri2());
		bool triIntersect3 = TriTriIntersection.TriTriOverlap.TriTriIntersect(quad1.GetTri2(), quad2.GetTri1());
		bool triIntersect4 = TriTriIntersection.TriTriOverlap.TriTriIntersect(quad1.GetTri2(), quad2.GetTri2());
		return triIntersect1 || triIntersect2 || triIntersect3 || triIntersect4;
	}

	public Vector2 bottomLeft = Vector2.zero;
	public Vector2 topLeft = Vector2.zero;
	public Vector2 topRight = Vector2.zero;
	public Vector2 bottomRight = Vector2.zero;

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

	public Quad(Vector2 bottomLeft, Vector2 topLeft, Vector2 topRight, Vector2 bottomRight) {
		this.bottomLeft = bottomLeft;
		this.topLeft = topLeft;
		this.bottomRight = bottomRight;
		this.topRight = topRight;

		CalculateBounds();
	}

	public Tri GetTri1() {
		return new Tri(bottomLeft, topLeft, topRight);
	}

	public Tri GetTri2() {
		return new Tri(topRight, bottomRight, bottomLeft);
	}

	public bool ContainsPoint(Vector2 point) {
		return Quad.ContainsPoint(this, point);
	}

	public bool OverlapQuad(Quad quad) {
		return Quad.OverlapQuads(this, quad);
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
