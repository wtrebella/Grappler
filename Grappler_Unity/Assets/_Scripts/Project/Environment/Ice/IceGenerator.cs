using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhitTerrain;

public class IceGenerator : Generator {
	[SerializeField] private ContourPair terrainPair;

	private void Awake() {
		BaseAwake();

		terrainPair.SignalPatternAdded += OnPatternAdded;
	}

	private void OnPatternAdded(ContourPairPatternType patternType, List<ContourSection> topSections, List<ContourSection> bottomSections) {
		if (patternType != ContourPairPatternType.Flat) return;

		ContourSection section = bottomSections.GetFirst();
		Ice ice = GenerateItem<Ice>();
		ice.SetSection(section);
	}
}
