using UnityEngine;
using System.Collections;

public class TreeGenerator : ItemOnTerrainGenerator {
	[SerializeField] private int maxTreesPerChunk = 10;
	[SerializeField] private float chunkProbability = 0.1f;
	[SerializeField] private float treeProbability = 0.8f;

	protected override void OnTerrainSectionAdded(WhitTerrainSection section) {

	}

//	private void HandleGroundChunkCreated(GroundChunk chunk) {
//		if (Random.value > chunkProbability) return;
//
//		var linePoints = chunk.GetEdgePoints();
//		int linePointsCount = linePoints.Count;
//		int startIndex = Random.Range(0, linePointsCount - 1);
//		int endIndex = Mathf.Min(linePointsCount - 1, startIndex + maxTreesPerChunk);
//		for (int i = startIndex; i < endIndex; i++) {
//			if (Random.value > treeProbability) continue;
//			Point point = linePoints[i];
//			GenerateTree(chunk, point);
//		}
//	}
//
//	private void GenerateTree(WhitTerrainSection section, Point chunkLinePoint) {
//		GeneratableItem item = GenerateItem();
//		Vector3 position = (Vector3)chunkLinePoint.vector;
//		Rigidbody2D rigid = item.GetComponentInChildren<Rigidbody2D>();
//		rigid.isKinematic = true;
//		item.transform.parent = section.transform;
//		item.transform.localScale = new Vector2(Random.Range(0.7f, 1.0f), Random.Range(0.7f, 1.0f));
//		item.transform.position = position;
//		rigid.isKinematic = false;
//	}
}
