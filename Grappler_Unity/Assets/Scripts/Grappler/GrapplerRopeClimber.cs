using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GrapplerRopeEndPoints))]
public class GrapplerRopeClimber : MonoBehaviour {
	[SerializeField] private Transform climbingObject;

	private GrapplerRopeEndPoints endPoints;

	private void Awake() {
		endPoints = GetComponent<GrapplerRopeEndPoints>();
	}

	private void Start() {
	
	}
	
	private void Update() {
		if (Input.GetKeyDown(KeyCode.UpArrow)) StartCoroutine(Climb());	
	}

	private IEnumerator Climb() {
		yield return new WaitForFixedUpdate();

		climbingObject.transform.position = endPoints.GetStartPoint();

		float lerp = 0;

		while (lerp < 1) {
			Vector2 startPoint = endPoints.GetStartPoint();
			Vector2 endPoint = endPoints.GetEndPoint();
			Vector2 delta = endPoint - startPoint;
			Vector2 direction = delta.normalized;
			float magnitude = delta.magnitude;
			Vector2 position = startPoint + lerp * direction * magnitude;
			climbingObject.transform.position = position;
			lerp += Time.fixedDeltaTime / 2;
			yield return new WaitForFixedUpdate();
		}
	}
}
