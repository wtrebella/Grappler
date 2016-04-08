using UnityEngine;
using System.Collections;

public class ItemOnTerrainGenerator : Generator {
	[SerializeField] private WhitTerrain terrain;

	private void Awake() {
		base.BaseAwake();
	}

	protected override void BaseAwake() {
		base.BaseAwake();
		terrain.SignalTerrainSectionAdded += OnTerrainSectionAdded;
		terrain.SignalTerrainSectionRemoved += OnTerrainSectionRemoved;
	}

	protected virtual void OnTerrainSectionAdded(WhitTerrainSection section) {
		
	}

	protected virtual void OnTerrainSectionRemoved(WhitTerrainSection section) {

	}

	protected GeneratableItem GenerateItemOnSection(WhitTerrainSection section, float relativeSurfaceDist) {
		GeneratableItem item = GenerateItem();
		item.transform.parent = section.transform;
		Vector3 position = section.GetSurfacePointAtRelativeDist(relativeSurfaceDist);
		position.z += 0.1f;
		item.transform.position = position;
		return item;
	}
}