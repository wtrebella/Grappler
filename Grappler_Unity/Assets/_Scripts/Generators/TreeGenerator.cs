using UnityEngine;
using System.Collections;

public class TreeGenerator : Generator {
	[SerializeField] private GroundChunkGenerator groundChunkGenerator;
	[SerializeField] private int maxTreesPerChunk = 10;
	[SerializeField] private float chunkProbability = 0.1f;
	[SerializeField] private float treeProbability = 0.8f;
	private void Awake() {
		base.BaseAwake();
		groundChunkGenerator.SignalGroundChunkGenerated += HandleGroundChunkCreated;
	}

	private void HandleGroundChunkCreated(GroundChunk chunk) {
		if (Random.value > chunkProbability) return;

		var linePoints = chunk.GetListOfLinePoints();
		int linePointsCount = linePoints.Count;
		int startIndex = Random.Range(0, linePointsCount - 1);
		int endIndex = Mathf.Min(linePointsCount - 1, startIndex + maxTreesPerChunk);
		for (int i = startIndex; i < endIndex; i++) {
			if (Random.value > treeProbability) continue;
			Point point = linePoints[i];
			GenerateTree(point);
		}
	}

	private void GenerateTree(Point chunkLinePoint) {
		GeneratableItem item = GenerateItem();
		Vector3 position = chunkLinePoint.pointVector.ToVector3();
		Rigidbody2D rigid = item.GetComponentInChildren<Rigidbody2D>();
		rigid.isKinematic = true;
		item.transform.localScale = new Vector2(Random.Range(0.6f, 1.0f), Random.Range(0.6f, 1.0f));
		float color = Random.Range(0.8f, 1.0f);
		item.GetComponentInChildren<tk2dSprite>().color = new Color(Random.Range(0.95f, 1.0f), Random.Range(0.95f, 1.0f), Random.Range(0.95f, 1.0f));
		item.transform.position = position;
		rigid.isKinematic = false;
	}
}
