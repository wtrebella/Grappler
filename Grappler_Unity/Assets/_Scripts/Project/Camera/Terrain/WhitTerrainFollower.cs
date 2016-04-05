using UnityEngine;
using System.Collections;

public class WhitTerrainFollower : MonoBehaviour {
	[SerializeField] private bool placeAtStart = true;
	[SerializeField] private WhitTerrainPair terrainPair;
	[SerializeField] private float speed = 10;
	[SerializeField] private float smoothDampTime = 0.3f;

	private Vector3 smoothDampVelocity;
	private float dist = 0;

	private void Start() {
		if (placeAtStart) PlaceAtStart();
	}

	private void Update() {
		if (!terrainPair.IsValid()) return;

		IncreaseDist(speed * Time.deltaTime);
	}

	private void PlaceAtStart() {
		transform.position = GetTargetPosition();
	}

	private void IncreaseDist(float distDelta) {
		dist += speed * Time.deltaTime;
		Vector3 smoothedPosition = GetSmoothedTargetPosition();
		transform.position = smoothedPosition;
	}

	private Vector3 GetTargetPosition() {
		Vector2 averagePoint = terrainPair.GetAveragePointAtDist(dist);
		Vector3 targetPosition = new Vector3(averagePoint.x, averagePoint.y, transform.position.z);
		return targetPosition;
	}

	private Vector3 GetSmoothedTargetPosition() {
		Vector2 targetPosition = GetTargetPosition();
		Vector3 targetPosition3D = (Vector3)targetPosition;
		targetPosition3D.z = transform.position.z;

		Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition3D, ref smoothDampVelocity, smoothDampTime);
		return smoothedPosition;
	}
}
