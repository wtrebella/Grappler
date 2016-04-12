using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemBetweenTerrainPairGenerator : Generator {
	[SerializeField] private WhitTerrainPair terrainPair;

	protected override void BaseAwake() {
		base.BaseAwake();
		terrainPair.SignalPatternAdded += OnPatternAdded;
	}

	private void OnPatternAdded(List<WhitTerrainSection> topSections, List<WhitTerrainSection> bottomSections) {
		float topDistStart = topSections.GetFirstItem().distStart;
		float topDistEnd = topSections.GetLastItem().distEnd;

		float bottomDistStart = bottomSections.GetFirstItem().distStart;
		float bottomDistEnd = bottomSections.GetLastItem().distEnd;

		float distStart = (topDistStart + bottomDistStart) / 2.0f;
		float distEnd = (topDistEnd + bottomDistEnd) / 2.0f;

		FloatRange distRange = new FloatRange(distStart, distEnd);

		OnPatternAdded(distRange);
	}

	protected virtual void OnPatternAdded(FloatRange distRange) {

	}

	protected GeneratableItem GenerateItem(float dist, float betweenPercent) {
		GeneratableItem item = GenerateItem();
		item.transform.parent = terrainPair.transform;
		Vector3 topPosition = terrainPair.topTerrain.GetPointAtDist(dist);
		Vector3 bottomPosition = terrainPair.bottomTerrain.GetPointAtDist(dist);
		Vector3 position = Vector3.Lerp(bottomPosition, topPosition, betweenPercent);
		position.z += 0.1f;
		item.transform.position = position;
		return item;
	}
}