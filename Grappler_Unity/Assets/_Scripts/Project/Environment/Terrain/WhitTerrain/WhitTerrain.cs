using UnityEngine;
using System.Collections;

public class WhitTerrain : MonoBehaviour {
	[SerializeField] private WhitTerrainLine terrainLine;
	[SerializeField] private WhitTerrainMesh terrainMesh;

	public void AddPart(float slope) {
		terrainLine.AddPart(slope);
		terrainLine.ClampSectionCount();
		terrainMesh.SetDirty();
	}

	public Vector2 GetFirstPoint() {
		return WhitTools.GetAveragePoint(terrainLine.GetFirstPoint());
	}

	public Vector2 GetLastPoint() {
		return WhitTools.GetAveragePoint(terrainLine.GetLastPoint());
	}

	public Vector2 GetAveragePointAtX(float x) {
		return WhitTools.GetAveragePoint(terrainLine.GetAveragePointAtX(x));
	}

	public bool HasSections() {
		return terrainLine.HasSections();
	}
}