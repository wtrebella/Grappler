using UnityEngine;
using System.Collections;

public class WhitTerrainLineFollower : MonoBehaviour {
	[SerializeField] private WhitTerrainGroup terrainLineGroup;
	[SerializeField] private float speed = 10;
	[SerializeField] private float smoothDampTime = 0.3f;

	private Vector3 smoothDampVelocity;
	private float x = 0;

	private void Update() {
		x += speed * Time.deltaTime;

		Vector3 smoothedPosition = GetSmoothedTargetPosition();
		transform.position = smoothedPosition;
	}

	private Vector2 GetTargetPosition() {
		Vector2 averagePoint = terrainLineGroup.GetAveragePointAtX(x);
		Vector3 targetPosition = new Vector3(x, averagePoint.y, transform.position.z);
		return targetPosition;
	}

	private Vector2 GetSmoothedTargetPosition() {
		Vector2 targetPosition = GetTargetPosition();
		Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothDampVelocity, smoothDampTime);
		return smoothedPosition;
	}
}
