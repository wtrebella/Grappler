using UnityEngine;
using System.Collections;

public class TreeGenerator : Generator {
	[SerializeField] private GroundChunkGenerator groundChunkGenerator;

	private void Awake() {
		base.BaseAwake();
		groundChunkGenerator.SignalGroundChunkGenerated += HandleGroundChunkCreated;
	}

	private void HandleGroundChunkCreated(GroundChunk chunk) {
		var linePoints = chunk.GetListOfLinePoints();
		float chance = 0.3f;
		for (int i = 5; i < linePoints.Count; i++) {
			Point point = linePoints[i];
			if (Random.value < chance) GenerateTree(point);
		}
	}

	private void GenerateTree(Point chunkLinePoint) {
		GeneratableItem item = GenerateItem();
		Vector3 position = chunkLinePoint.pointVector.ToVector3();
		Rigidbody2D rigid = item.GetComponentInChildren<Rigidbody2D>();
		rigid.isKinematic = true;
		item.transform.position = position;
		rigid.isKinematic = false;
	}
}
