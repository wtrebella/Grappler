using UnityEngine;
using System.Collections;
using System;

public class GameCamera : MonoBehaviour {
	[SerializeField] private WhitUpdateType updateType = WhitUpdateType.Update;
	[SerializeField] private Transform focusObject;
	[SerializeField] private WhitTerrainPair terrainPair;
	[SerializeField] private Camera cam;

	public void SetUpdateType(WhitUpdateType newUpdateType) {
		updateType = newUpdateType;
	}

	public void UpdateMovementImmediateNow() {
		UpdateMovementImmediate();
	}

	private void Awake() {

	}

	private void Update() {
		if (updateType != WhitUpdateType.Update) return;

		UpdateMovement();
	}

	private void FixedUpdate() {
		if (updateType != WhitUpdateType.FixedUpdate) return;

		UpdateMovement();
	}

	private void UpdateMovement() {
		UpdateMovementImmediate();
	}
		
	private void UpdateMovementImmediate() {
		transform.position = GetTargetPosition();
	}

	private float GetObjectToThisDistance() {
		return GetObjectToThisVector().magnitude;
	}

	private Vector3 GetObjectToThisDirection() {
		return GetObjectToThisVector().normalized;
	}

	private Vector2 GetObjectToThisVector() {
		return transform.position - focusObject.position;
	}

	private Vector3 GetTargetPosition() {
		Vector3 objectPosition = focusObject.position;
		Vector3 terrainPoint = (Vector3)terrainPair.GetPointAtX(objectPosition.x);
		terrainPoint.z = transform.position.z;
		return terrainPoint;
	}


}
