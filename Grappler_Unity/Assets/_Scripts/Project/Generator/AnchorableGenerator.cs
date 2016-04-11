using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AnchorableGenerator : Generator {
	[SerializeField] private WhitTerrain terrain;

	private static int currentAnchorableID = 0;

	private void Awake() {
		BaseAwake();
		terrain.SignalTerrainSectionAdded += OnSectionAdded;
	}

	private void GenerateAnchorables(WhitTerrainSection section) {
		foreach (Vector2 point in section.allPoints) CreateAnchorableAtPoint(section, point);
	}

	private void CreateAnchorableAtPoint(WhitTerrainSection section, Vector2 point) {
		Anchorable anchorable = (Anchorable)GenerateItem();
		anchorable.transform.parent = section.transform;
		anchorable.transform.localScale = point;
		anchorable.SetAnchorableID(currentAnchorableID++);
	}

	private void OnSectionAdded(WhitTerrainSection section) {
		GenerateAnchorables(section);
	}
}
