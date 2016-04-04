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

	public Vector2 GetDirectionAtEnd() {
		return WhitTools.GetSumPoint(GetDirectionsAtEnd()).normalized;
	}

	public void AddStraight(float slope, float length) {
		foreach (WhitTerrain terrain in terrains) terrain.AddStraight(slope, length);
	}

	public void AddCurve(float targetSlope) {
		foreach (WhitTerrain terrain in terrains) terrain.AddCurve(targetSlope);
	}

	public void AddCurveThenStraight(float targetSlope, float straightLength) {
		foreach (WhitTerrain terrain in terrains) terrain.AddCurveThenStraight(targetSlope, straightLength);
	}

	public bool AllTerrainIsValid() {
		foreach (WhitTerrain terrainLine in terrains) {
			if (!terrainLine.TerrainIsValid()) return false;
		}
		return true;
	}

	private Vector2[] GetDirectionsAtEnd() {
		Vector2[] directions = new Vector2[terrains.Length];
		for (int i = 0; i < terrains.Length; i++) {
			WhitTerrain terrain = terrains[i];
			directions[i] = terrain.GetDirectionAtEnd();
		}
		return directions;
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