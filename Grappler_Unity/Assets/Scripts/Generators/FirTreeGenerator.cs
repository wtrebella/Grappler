using UnityEngine;
using System.Collections;

public class FirTreeGenerator : MonoBehaviour {
	[SerializeField] private float minDistToMountain = 3;
	[SerializeField] private float maxDistToMountain = 10;
	[SerializeField] private Breakable firTreePrefab;
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;

	private void Awake() {
		mountainChunkGenerator.SignalMountainChunkCreated += HandleMountainChunkCreated;
	}

	private void HandleMountainChunkCreated(MountainChunk chunk) {
		var linePoints = chunk.GetListOfMacroLinePoints();
		float chance = 0.1f;
		for (int i = 5; i < linePoints.Count; i++) {
			Point point = linePoints[i];
			if (Random.value < chance) GenerateTree(point);
		}
	}

	private void GenerateTree(Point chunkLinePoint) {
		float distance = Random.Range(minDistToMountain, maxDistToMountain);
		Vector3 linePoint = chunkLinePoint.pointVector.ToVector3();
		Vector3 distanceVector = new Vector3(0, 0, distance);
		Vector3 position = linePoint - distanceVector;
		Breakable firTree = firTreePrefab.Spawn();
		Rigidbody2D rigid = firTree.GetComponentInChildren<Rigidbody2D>();
		rigid.isKinematic = true;
		firTree.transform.parent = transform;
		firTree.transform.position = position;
		rigid.isKinematic = false;
	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
