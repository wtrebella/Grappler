using UnityEngine;
using System.Collections;

public class CragGenerator : ItemOnTerrainGenerator {
	[SerializeField] private float cragProbibility = 0.5f;
	[SerializeField] private FloatRange deltaDistRange = new FloatRange(3.0f, 30.0f);

	protected override void OnTerrainSectionAdded(WhitTerrainSection section) {
		if (Random.value < cragProbibility) GenerateCrag(section);
	}

	protected override void OnTerrainSectionRemoved(WhitTerrainSection section) {
		RecycleItemsOnSection<Crag>(section);
	}

	private void GenerateCrag(WhitTerrainSection section) {
		GenerateItemOnSection(section, deltaDistRange.GetRandom());
	}
}
