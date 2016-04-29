using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemBetweenTerrainPairGenerator : Generator {
	[SerializeField] protected WhitTerrainPair terrainPair;

	protected override void BaseAwake() {
		base.BaseAwake();
		terrainPair.SignalPatternAdded += OnPatternAdded;
	}

	private void OnPatternAdded(WhitTerrainPairPatternType patternType, List<WhitTerrainSection> topSections, List<WhitTerrainSection> bottomSections) {
		float topDistStart = topSections.GetFirst().distStart;
		float topDistEnd = topSections.GetLast().distEnd;

		float bottomDistStart = bottomSections.GetFirst().distStart;
		float bottomDistEnd = bottomSections.GetLast().distEnd;

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

	protected T GenerateItem<T>(float dist, float betweenPercent) where T : GeneratableItem  {
		return (T)GenerateItem(dist, betweenPercent);
	}
}