using UnityEngine;
using System.Collections;

public class CloudGenerator : MonoBehaviour {
	[SerializeField] private IntRange linePointBetweenRange;
	[SerializeField] private FloatRange distanceFromMountainRange;
	[SerializeField] private FloatRange zRange;
	[SerializeField] private Cloud cloudPrefab;

	private void Awake() {

	}

	private void Start() {
	
	}
	
	private void Update() {
		
	}

	private void GenerateCloud(Point chunkLinePoint) {
		float distance = distanceFromMountainRange.GetRandom();
		float distanceFromCamera = zRange.GetRandom();
		Vector3 linePoint = chunkLinePoint.pointVector.ToVector3();
		Vector3 distanceVector = new Vector3(0, 0, distance);
		Vector3 position = linePoint - distanceVector;
		position.z += distanceFromCamera;
		Cloud cloud = cloudPrefab.Spawn();
		cloud.transform.parent = transform;
		cloud.transform.position = position;
	}
}
