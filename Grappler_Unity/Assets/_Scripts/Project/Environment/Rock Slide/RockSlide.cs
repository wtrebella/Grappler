using UnityEngine;
using System.Collections;
using UnityEngine.Events;

// TODO: get rid of hasStarted and extract it out into game manager

public class RockSlide : MonoBehaviour {
	[SerializeField] private Transform startPoint;
	[SerializeField] private WhitUpdateType updateType = WhitUpdateType.Update;
	[SerializeField] private Transform playerBody;
	[SerializeField] private float slowZoneWidth = 40;
	[SerializeField] private float slowZoneSpeed = 80;
	[SerializeField] private float fastZoneSpeed = 120;

	private bool hasStarted = false;
	private bool isMoving = false;

	private void Start() {
		if (InputManager.instance != null) {
			InputManager.instance.SignalRightTouchDown += OnRightTouchDown;
		}
	}

	public void StartMoving() {
		isMoving = true;
	}

	public void StopMoving() {
		isMoving = false;
	}

	private float GetDistanceToPlayer() {
		return Mathf.Abs(startPoint.position.x - playerBody.position.x);
	}

	private bool IsInSlowZone() {
		return GetDistanceToPlayer() < slowZoneWidth;
	}

	private void Update() {
		if (updateType != WhitUpdateType.Update) return;
		if (!hasStarted) return;
		if (isMoving) MoveUpdate();
	}

	private void OnRightTouchDown() {
		if (!hasStarted) {
			hasStarted = true;
			StartMoving();
		}
	}

	private void FixedUpdate() {
		if (updateType != WhitUpdateType.FixedUpdate) return;
		if (!hasStarted) return;
		if (isMoving) MoveFixedUpdate();
	}
	
	private void MoveUpdate() {
		float deltaTime = Time.deltaTime * Time.timeScale;
		Move(deltaTime);
	}

	private void MoveFixedUpdate() {
		float deltaTime = Time.fixedDeltaTime * Time.timeScale;
		Move(deltaTime);
	}

	private void Move(float deltaTime) {
		float speed;
		if (IsInSlowZone()) speed = slowZoneSpeed;
		else speed = fastZoneSpeed;
		Vector3 position = transform.position;
		position.x += speed * deltaTime;
		transform.position = position;
	}
}
