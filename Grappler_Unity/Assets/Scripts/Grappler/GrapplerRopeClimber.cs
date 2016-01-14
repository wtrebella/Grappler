using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GrapplerRopeEndPoints))]
public class GrapplerRopeClimber : MonoBehaviour {
	[SerializeField] private Transform grabPoint;
	[SerializeField] private Transform bodySprite;
	[SerializeField] private Transform feetSprite;
	[SerializeField] private float moveToStartPointSmoothTime = 0.15f;
	[SerializeField] private float moveToStartPointSpeed = 50;
	[SerializeField] private float climbSmoothTime = 0.15f;
	[SerializeField] private float climbSpeed = 0.5f;

	private Transform climbingObject;
	private Vector3 moveToStartPointVelocity;
	private Vector3 climbVelocity;
	private GrapplerRopeEndPoints endPoints;

	private void Awake() {
		endPoints = GetComponent<GrapplerRopeEndPoints>();
	}

	private void Start() {
	
	}
	
	private void Update() {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			PrepareToClimb();
			StartCoroutine(MoveToStartPointThenClimb());	
		}
	}

	private void PrepareToClimb() {
		Transform spriteContainer = new GameObject("Sprite Container").transform;
		spriteContainer.position = grabPoint.position;
		bodySprite.parent = spriteContainer;
		feetSprite.parent = bodySprite;
		climbingObject = spriteContainer;
	}

	private IEnumerator MoveToStartPointThenClimb() {
		yield return StartCoroutine(MoveToStartPoint());
		StartCoroutine(Climb());
	}
	
	private IEnumerator MoveToStartPoint() {
		yield return new WaitForFixedUpdate();
		
		float minDist = 0.05f;
		float dist = endPoints.GetDistanceBetweenEndPoints();
		float initialDist = dist;
		float minSpeedEase = 0.5f;
		float speedEase = 1.0f;
		float speedEaseRange = speedEase - minSpeedEase;

		while (dist > minDist) {
			Vector2 currentPosition = climbingObject.position;
			Vector2 endPoint = endPoints.GetStartPoint();
			Vector2 delta = endPoint - currentPosition;
			Vector2 direction = delta.normalized;
			dist = delta.magnitude;
			float distRatio = dist / initialDist;

			Vector2 position = currentPosition + direction * moveToStartPointSpeed * speedEase * Time.fixedDeltaTime;
			climbingObject.transform.position = Vector3.SmoothDamp(currentPosition, position, ref moveToStartPointVelocity, moveToStartPointSmoothTime);

			speedEase = minSpeedEase + speedEaseRange * distRatio;

			yield return new WaitForFixedUpdate();
		}
	}

	private IEnumerator Climb() {
		yield return new WaitForFixedUpdate();

		float lerp = 0;
		
		while (lerp < 1) {
			Vector2 startPoint = endPoints.GetStartPoint();
			Vector2 endPoint = endPoints.GetEndPoint();
			Vector2 delta = endPoint - startPoint;
			Vector2 direction = delta.normalized;
			float magnitude = delta.magnitude;
			Vector2 position = startPoint + lerp * direction * magnitude;
			climbingObject.transform.position = Vector3.SmoothDamp(climbingObject.transform.position, position, ref climbVelocity, climbSmoothTime);
			lerp += Time.fixedDeltaTime * climbSpeed;
			yield return new WaitForFixedUpdate();
		}
	}
}
