using UnityEngine;
using System.Collections;

public class PerspectiveCameraAnchorAdjuster : MonoBehaviour {
	Camera cam;

	void Awake() {
		cam = GetComponentInParent<Camera>();
		if (cam == null) Debug.LogError("not attached to a camera object!");
		UpdateAnchors();
	}

	void Start () {
	
	}
	
	void Update () {
		UpdateAnchors();
	}

	private void UpdateAnchors() {
		Vector3 p = transform.localPosition;
		p.z = -cam.transform.position.z;
		transform.localPosition = p;
	}
}
