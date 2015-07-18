using UnityEngine;
using System.Collections;

public class GameScreen : MonoBehaviour {
	public static GameScreen instance;

	[SerializeField] private tk2dCameraAnchor lowerLeftAnchor;
	[SerializeField] private tk2dCameraAnchor upperRightAnchor;

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
		get {
			return lowerLeftAnchor.transform.position;
		}
	}
	
	public float width {
		get {
			return upperRightAnchor.transform.position.x - lowerLeftAnchor.transform.position.x;
		}
	}

	public float height {
		get {
			return upperRightAnchor.transform.position.y - lowerLeftAnchor.transform.position.y;
		}
	}
}
