using UnityEngine;
using System.Collections;

public class BigTreeGenerator : ItemOnTerrainGenerator {
	[SerializeField] private float treeProbability = 0.5f;
	[SerializeField] private FloatRange deltaDistRange = new FloatRange(3.0f, 30.0f);

	protected override void OnTerrainSectionAdded(WhitTerrainSection section) {
		if (Random.value < treeProbability) GenerateTree(section);
	}

	protected override void OnTerrainSectionRemoved(WhitTerrainSection section) {
		RecycleItemsOnSection<BigTree>(section);
	}

	private void GenerateTree(WhitTerrainSection section) {
		GenerateItemOnSection(section, deltaDistRange.GetRandom());
	}
}
