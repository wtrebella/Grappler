using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using WhitTerrain;

public class AnchorableGenerator : ItemOnTerrainGenerator {
	private static int currentAnchorableID = 0;

	protected override void OnTerrainSectionAdded(ContourSection section) {
		GenerateAnchorables(section);
	}

	protected override void OnTerrainSectionRemoved(ContourSection section) {
		RecycleItemsOnSection<Anchorable>(section);
	}

	private void GenerateAnchorables(ContourSection section) {
		foreach (Vector2 point in section.allPoints) CreateAnchorableAtPoint(section, point);
	}

	private void CreateAnchorableAtPoint(ContourSection section, Vector2 point) {
		Anchorable anchorable = (Anchorable)GenerateItem();
		anchorable.transform.parent = section.transform;
		anchorable.transform.localPosition = point;
		anchorable.SetAnchorableID(currentAnchorableID++);
	}
}
