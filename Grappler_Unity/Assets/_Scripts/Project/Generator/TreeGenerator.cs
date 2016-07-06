using UnityEngine;
using System.Collections;
using WhitTerrain;

public class TreeGenerator : ItemOnTerrainGenerator {
	[SerializeField] private float sectionProbability = 0.5f;
	[SerializeField] private FloatRange deltaDistRange = new FloatRange(3.0f, 30.0f);

	protected override void OnTerrainSectionAdded(ContourSection section) {
		if (Random.value < sectionProbability) GenerateTrees(section);
	}

	protected override void OnTerrainSectionRemoved(ContourSection section) {
		RecycleItemsOnSection<FirTree>(section);
	}

	private void GenerateTrees(ContourSection section) {
		GenerateItemsOnSection(section, deltaDistRange);
	}
}
