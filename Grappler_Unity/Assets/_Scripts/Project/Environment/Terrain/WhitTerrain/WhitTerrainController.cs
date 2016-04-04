using UnityEngine;
using System.Collections;

public class WhitTerrainController : MonoBehaviour {
//	[SerializeField] private Transform followPoint;
	[SerializeField] private WhitTerrainGroup terrainGroup;
//	[SerializeField] private float smoothDampTime = 0.3f;
//	[SerializeField] private float distanceThreshold = 0.3f;

//	private bool isOn = true;
	private Vector2 smoothDampVelocity;

	private IEnumerator Start() {
		while (!terrainGroup.HasSections()) yield return null;

		while (true) {
			terrainGroup.AddPart(Random.Range(-0.3f, 0.3f));
			yield return new WaitForSeconds(1);
		}
	}

//	private void Update() {
//		if (isOn) {
//			UpdateLineManagers();
//			if (!ShouldBeOn()) TurnOff();
//		}
//		else {
//			if (ShouldBeOn()) TurnOn();
//		}
//	}


//
//	private void UpdateLineManagers() {
//		terrainGroup.AddPart((GetPosition() - (Vector2)transform.position).normalized);
//	}

//	private bool ShouldBeOn() {
//		return GetDistance() > distanceThreshold;
//	}
//
//	private void TurnOn() {
//		isOn = true;
//	}
//
//	private void TurnOff() {
//		isOn = false;
//	}

//	private Vector2 GetPosition() {
//		return followPoint.position;
//		Vector2 currentPosition = transform.position;
//		Vector2 targetPosition = followPoint.position;
//		Vector2 smoothedPosition = Vector2.SmoothDamp(
//			currentPosition, 
//			targetPosition, 
//			ref smoothDampVelocity, 
//			smoothDampTime
//		);
//		return smoothedPosition;
//	}
}