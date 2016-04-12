using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudGenerator : Generator {
	[SerializeField] private FloatRange camDistanceBetweenClouds;
	[SerializeField] private FloatRange zRange;

	private Vector3 camPositionAtLastCloudCreation = Vector3.zero;
	private float camDistanceToNextCloud = 0;

	private bool NeedsNewCloud() {
		float distance = GetDistanceMovedSinceLastCloud();
		return distance > camDistanceToNextCloud;
	}

	private float GetDistanceMovedSinceLastCloud() {
		Vector3 camPosition = ScreenUtility.instance.cam.transform.position;
		return (camPositionAtLastCloudCreation - camPosition).magnitude;
	}

	private void Start() {
		for (int i = 0; i < 5; i++) GenerateCloudInRandomScreenSpot();
	}
	
	private void Update() {
		if (NeedsNewCloud()) GenerateCloudToRightOfScreen();
	}

	private void GenerateCloudInRandomScreenSpot() {
		Cloud cloud = (Cloud)GenerateItem();
		float z = zRange.GetRandom();
		Vector3 position = ScreenUtility.instance.GetRandomWorldPoint(z);
		cloud.transform.position = position;
	}

	private void GenerateCloudToRightOfScreen() {
		float x = Screen.width + 1000;
		float y = Random.Range(250, Screen.height + 500);
		float z = zRange.GetRandom();
		Vector3 newCloudScreenPosition = new Vector3(x, y, z);
		Vector3 newCloudWorldPosition = ScreenUtility.instance.ScreenPointToWorldPoint(newCloudScreenPosition, z);
		Cloud cloud = (Cloud)GenerateItem();
		cloud.transform.position = newCloudWorldPosition;
		camPositionAtLastCloudCreation = ScreenUtility.instance.cam.transform.position;
		CalculateCamDistanceToNextCloud();
	}

	private void CalculateCamDistanceToNextCloud() {
		camDistanceToNextCloud = camDistanceBetweenClouds.GetRandom();
	}
}
