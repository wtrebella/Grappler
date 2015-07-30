using UnityEngine;
using System.Collections;

public class GameScreen : MonoBehaviour {
	public static GameScreen instance;

	[SerializeField] private tk2dCameraAnchor lowerLeftAnchor;
	[SerializeField] private tk2dCameraAnchor upperRightAnchor;
	[SerializeField] private float onscreenMargin = 10;

	private void Awake() {
		instance = this;
	}

	public Vector2 center {
		get {
			Vector2 centerPoint = Vector2.zero;
			centerPoint.x = origin.x + width / 2f;
			centerPoint.y = origin.y + height / 2f;
			return centerPoint;
		}
	}

	public Vector2 origin {
		get {return lowerLeftAnchor.transform.position;}
	}
	
	public float width {
		get {return upperRightAnchor.transform.position.x - lowerLeftAnchor.transform.position.x;}
	}

	public float height {
		get {return upperRightAnchor.transform.position.y - lowerLeftAnchor.transform.position.y;}
	}

	public float minX {
		get {return origin.x;}
	}

	public float maxX {
		get {return lowerRight.x;}
	}

	public float minY {
		get {return lowerLeft.y;}
	}

	public float maxY {
		get {return upperLeft.y;}
	}

	public float minMarginX {
		get {return lowerLeftWithMargin.x;}
	}
	
	public float maxMarginX {
		get {return lowerRightWithMargin.x;}
	}
	
	public float minMarginY {
		get {return lowerLeftWithMargin.y;}
	}
	
	public float maxMarginY {
		get {return upperLeftWithMargin.y;}
	}

	public Vector2 lowerLeft {
		get {return origin;}
	}

	public Vector2 lowerRight {
		get {return new Vector2(upperRightAnchor.transform.position.x, origin.y);}
	}

	public Vector2 upperLeft {
		get {return new Vector2(origin.x, upperRightAnchor.transform.position.y);}
	}

	public Vector2 upperRight {
		get {return upperRightAnchor.transform.position;}
	}

	public Vector2 lowerLeftWithMargin {
		get {return new Vector2(origin.x - onscreenMargin, origin.y - onscreenMargin);}
	}
	
	public Vector2 lowerRightWithMargin {
		get {return new Vector2(upperRightAnchor.transform.position.x + onscreenMargin, origin.y - onscreenMargin);}
	}
	
	public Vector2 upperLeftWithMargin {
		get {return new Vector2(origin.x - onscreenMargin, upperRightAnchor.transform.position.y + onscreenMargin);}
	}
	
	public Vector2 upperRightWithMargin {
		get {return new Vector2(upperRightAnchor.transform.position.x + onscreenMargin, upperRightAnchor.transform.position.y + onscreenMargin);}
	}

	public bool GetIsOnscreenWithMarginX(float x) {
		return x >= lowerLeftWithMargin.x && x <= lowerRightWithMargin.x;
	}

	public bool GetIsOnscreenWithMarginY(float y) {
		return y >= lowerLeftWithMargin.y && y <= upperLeftWithMargin.y;
	}

	public bool GetIsOnscreenWithMargin(Vector2 point) {
		return GetIsOnscreenWithMarginX(point.x) && GetIsOnscreenWithMarginY(point.y);
	}
}
