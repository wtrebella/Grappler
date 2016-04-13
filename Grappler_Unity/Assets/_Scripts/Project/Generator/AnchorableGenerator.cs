using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AnchorableGenerator : ItemOnTerrainGenerator {
	private static int currentAnchorableID = 0;

	protected override void OnTerrainSectionAdded(WhitTerrainSection section) {
		GenerateAnchorables(section);
	}

	protected override void OnTerrainSectionRemoved(WhitTerrainSection section) {
		RecycleItemsOnSection<Anchorable>(section);
	}

	private void GenerateAnchorables(WhitTerrainSection section) {
		foreach (Vector2 point in section.allPoints) CreateAnchorableAtPoint(section, point);
	}

	private void CreateAnchorableAtPoint(WhitTerrainSection section, Vector2 point) {
		Anchorable anchorable = (Anchorable)GenerateItem();
		anchorable.transform.parent = section.transform;
		anchorable.transform.localPosition = point;
		anchorable.SetAnchorableID(currentAnchorableID++);
	}
}
