using UnityEngine;
using System.Collections;

public class WhitTerrainGroup : MonoBehaviour {
	[SerializeField] private WhitTerrain[] terrains;

	public Vector2 GetFirstPoint() {
		return WhitTools.GetAveragePoint(GetFirstPoints());
	}

	public Vector2 GetLastPoint() {
		return WhitTools.GetAveragePoint(GetLastPoints());
	}

	public Vector2 GetAveragePointAtX(float x) {
		return WhitTools.GetAveragePoint(GetAveragePointsAtX(x));
	}

	public void AddPart(float slope) {
		foreach (WhitTerrain terrainLine in terrains) terrainLine.AddPart(slope);
	}

	public bool HasSections() {
		foreach (WhitTerrain terrainLine in terrains) {
			if (!terrainLine.HasSections()) return false;
		}
		return true;
	}

	private Vector2[] GetLastPoints() {
		Vector2[] lastPoints = new Vector2[terrains.Length];
		for (int i = 0; i < terrains.Length; i++) {
			WhitTerrain terrainLine = terrains[i];
			lastPoints[i] = terrainLine.GetLastPoint();
		}
		return lastPoints;
	}

	private Vector2[] GetFirstPoints() {
		Vector2[] firstPoints = new Vector2[terrains.Length];
		for (int i = 0; i < terrains.Length; i++) {
			WhitTerrain terrainLine = terrains[i];
			firstPoints[i] = terrainLine.GetFirstPoint();
		}
		return firstPoints;
	}

	private Vector2[] GetAveragePointsAtX(float x) {
		Vector2[] averagePoints = new Vector2[terrains.Length];
		for (int i = 0; i < terrains.Length; i++) {
			WhitTerrain terrainLine = terrains[i];
			averagePoints[i] = terrainLine.GetAveragePointAtX(x);
		}
		return averagePoints;
	}
}