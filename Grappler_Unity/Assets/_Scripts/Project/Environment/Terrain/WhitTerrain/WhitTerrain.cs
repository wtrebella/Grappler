using UnityEngine;
using System.Collections;

public class WhitTerrain : MonoBehaviour {
	[SerializeField] private WhitTerrainLine terrainLine;
	[SerializeField] private WhitTerrainMesh terrainMesh;

	private bool isDirty = false;

	public void AddStraight(float slope, float length) {
		terrainLine.AddStraight(slope, length);
		isDirty = true;
	}

	public void AddCurve(float targetSlope) {
		terrainLine.AddCurve(targetSlope);
		isDirty = true;
	}

	public void AddCurveThenStraight(float targetSlope, float straightLength) {
		AddCurve(targetSlope);
		AddStraight(targetSlope, straightLength);
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

	public Vector2 GetDirectionAtEnd() {
		return terrainLine.GetLastSectionDirection();
	}

	public bool TerrainIsValid() {
		return terrainLine.HasSections();
	}

	private void Update() {
		if (isDirty) UpdateTerrain();
	}

	private void UpdateTerrain() {
		isDirty = false;
		terrainLine.ClampSectionCount();
		terrainMesh.SetDirty();
	}
}