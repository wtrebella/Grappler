using UnityEngine;
using System.Collections;

public class TreeGenerator : ItemOnTerrainGenerator {
	[SerializeField] private float sectionProbability = 0.5f;
	[SerializeField] private FloatRange deltaDistRange = new FloatRange(3.0f, 30.0f);

	protected override void OnTerrainSectionAdded(WhitTerrainSection section) {
		if (Random.value < sectionProbability) GenerateTrees(section);
	}

	private void GenerateTrees(WhitTerrainSection section) {
		GenerateItemsOnSection(section, deltaDistRange);
	}
}
