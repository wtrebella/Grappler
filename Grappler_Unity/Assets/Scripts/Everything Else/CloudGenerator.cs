using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudGenerator : MonoBehaviour {
	[SerializeField] private FloatRange camDistanceBetweenClouds;
	[SerializeField] private FloatRange zRange;
	[SerializeField] private Cloud cloudPrefab;
	[SerializeField] private int maxClouds = 20;

	private List<Cloud> clouds;
	private Vector3 camPositionAtLastCloudCreation = Vector3.zero;
	private float camDistanceToNextCloud = 0;

	private bool NeedsNewCloud() {
		float distance = GetDistanceMovedSinceLastCloud();
		return distance > camDistanceToNextCloud;
	}

	private float GetDistanceMovedSinceLastCloud() {
		Vector3 camPosition = GameScreen.instance.cam.transform.position;
		return (camPositionAtLastCloudCreation - camPosition).magnitude;
	}

	private void Awake() {
		clouds = new List<Cloud>();
	}

	private void Start() {
		for (int i = 0; i < 5; i++) GenerateCloudInRandomScreenSpot();
	}
	
	private void Update() {
		if (NeedsNewCloud()) GenerateCloudToRightOfScreen();
	}

	private void GenerateCloudInRandomScreenSpot() {
		float z = zRange.GetRandom();
		Vector3 position = GameScreen.instance.GetRandomWorldPoint(z);
		Cloud cloud = cloudPrefab.Spawn();
		cloud.transform.parent = transform;
		cloud.transform.position = position;
		clouds.Add(cloud);
		if (clouds.Count > maxClouds) RecycleFirstCloud();
	}

	private void RecycleFirstCloud() {
		Cloud firstCloud = clouds[0];
		clouds.Remove(firstCloud);
		firstCloud.Recycle();
	}

	private void GenerateCloudToRightOfScreen() {
		float x = Screen.width + 1000;
		float y = Random.Range(-500, Screen.height + 500);
		float z = zRange.GetRandom();
		Vector3 newCloudScreenPosition = new Vector3(x, y, z);
		Vector3 newCloudWorldPosition = GameScreen.instance.ScreenPointToWorldPoint(newCloudScreenPosition, z);
		Cloud cloud = cloudPrefab.Spawn();
		cloud.transform.parent = transform;
		cloud.transform.position = newCloudWorldPosition;
		camPositionAtLastCloudCreation = GameScreen.instance.cam.transform.position;
		CalculateCamDistanceToNextCloud();
	}

	private void CalculateCamDistanceToNextCloud() {
		camDistanceToNextCloud = camDistanceBetweenClouds.GetRandom();
	}
}
