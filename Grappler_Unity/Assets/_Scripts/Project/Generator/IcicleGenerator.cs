using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhitTerrain;

public class IcicleGenerator : ItemOnTerrainGenerator {
	[SerializeField] private FloatRange deltaDistRange = new FloatRange(1.0f, 20.0f);

	protected override void OnTerrainSectionAdded(ContourSection section) {
		GenerateIcicles(section);
	}

	protected override void OnTerrainSectionRemoved(ContourSection section) {
		RecycleItemsOnSection<SmallIcicle>(section);
	}

	private void GenerateIcicles(ContourSection section) {
		GenerateItemsOnSection(section, deltaDistRange);
	}
}