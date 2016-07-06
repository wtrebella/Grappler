using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhitTerrain;

public class WarningSignGenerator : ItemOnTerrainGenerator {
	[SerializeField] private ContourPair terrainPair;

	protected override void BaseAwake() {
		base.BaseAwake();
		terrainPair.SignalPatternAdded += OnPatternAdded;
	}

	private void OnPatternAdded(ContourPairPatternType patternType, List<ContourSection> topSections, List<ContourSection> bottomSections) {
		if (patternType == ContourPairPatternType.End) {
			GenerateItemOnSection(bottomSections.GetFirst(), 0);
		}
	}
}
