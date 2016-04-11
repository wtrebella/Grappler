using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IcicleGenerator : ItemOnTerrainGenerator {
	[SerializeField] private FloatRange deltaDistRange = new FloatRange(1.0f, 20.0f);

	protected override void OnTerrainSectionAdded(WhitTerrainSection section) {
		GenerateIcicles(section);
	}

	private void GenerateIcicles(WhitTerrainSection section) {
		var items = GenerateItemsOnSection(section, deltaDistRange);
		foreach (GeneratableItem item in items) {
			float xScale = Random.Range(0.2f, 1f);
			float yScale = Random.Range(0.2f, 1f);
			Vector3 scale = new Vector3(xScale, yScale, 1);
			item.transform.localScale = scale;
		}
	}
}