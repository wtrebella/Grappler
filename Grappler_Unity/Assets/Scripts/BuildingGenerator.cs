using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class BuildingGenerator : MonoBehaviour {
	public Action<Building> SignalCreatedFirstBuilding;
	public Action<Building> SignalCreatedBuilding;

	[SerializeField] Building buildingPrefab;
	[SerializeField] float minWidth = 5;
	[SerializeField] float maxWidth = 30;
	[SerializeField] float minHeight = 50;
	[SerializeField] float maxHeight = 100;
	[SerializeField] float maxHeightDifference = 50;

	private List<Building> buildings;

	private void Awake() {
		buildings = new List<Building>();
	}

	private void Start() {
		CreateBuildingsIfNeeded();
	}

	private void Update() {
		CreateBuildingsIfNeeded();
		RecycleOffScreenBuildings();
	}

	private void CreateBuilding() {
		float x = GetNextX();
		Vector2 size = GetNextSize();
		Color color = GetNextColor();
		Vector2 position = new Vector2(x, 0);
		
		BuildingAttributes buildingAttributes = new BuildingAttributes();
		buildingAttributes.size = size;
		buildingAttributes.color = color;
		buildingAttributes.position = position;
		
		CreateBuilding(buildingAttributes);
	}
	
	private void CreateBuilding(BuildingAttributes buildingAttributes) {
		Building building = buildingPrefab.Spawn();
		building.transform.parent = transform;
		building.SetBuildingAttributes(buildingAttributes);
		buildings.Add(building);
		if (SignalCreatedBuilding != null) SignalCreatedBuilding(building);
		if (buildings.Count == 1) {
			if (SignalCreatedFirstBuilding != null) SignalCreatedFirstBuilding(building);
		}
	}

	private void RecycleOffScreenBuildings() {
		List<int> indicesOfBuildingsToRecycle = GetIndicesOfBuildingsToRecycle();

		for (int i = 0; i < indicesOfBuildingsToRecycle.Count; i++) {
			RecycleBuildingAt(indicesOfBuildingsToRecycle[i]);
		}
	}

	private List<int> GetIndicesOfBuildingsToRecycle() {
		List<int> indicesOfBuildingsToRecycle = new List<int>();
		
		for (int i = 0; i < buildings.Count; i++) {
			Building building = buildings[i];
			if (building.IsOffLeftOfScreen()) indicesOfBuildingsToRecycle.Add(i);
			else break;
		}

		return indicesOfBuildingsToRecycle;
	}

	private void RecycleBuildingAt(int index) {
		Building building = buildings[index];
		buildings.RemoveAt(index);
		building.Recycle();
	}

	private void CreateBuildingsIfNeeded() {
		while (NeedToCreateBuilding()) CreateBuilding();
	}

	private bool NeedToCreateBuilding() {
		if (BuildingsExist()) return !GetLastBuilding().IsOffRightOfScreen();
		else return true;
	}

	private Building GetLastBuilding() {
		WhitTools.Assert(buildings.Count > 0, "no buildings to get!");

		return buildings[buildings.Count - 1];
	}

	private bool BuildingsExist() {
		return buildings.Count > 0;
	}

	private Vector2 GetNextSize() {
		float width = GetNextWidth();
		float height = GetNextHeight();
		
		return new Vector2(width, height);
	}

	private float GetNextX() {
		if (BuildingsExist()) return GetLastBuilding().bottomRightCorner.x;
		else return 0;
	}

	private float GetNextWidth() {
		return UnityEngine.Random.Range(minWidth, maxWidth);
	}

	private float GetNextHeight() {
		float height = 0;

		if (BuildingsExist()) {
			Building lastBuilding = GetLastBuilding();
			float minNegativeDelta = Mathf.Max(minHeight - lastBuilding.height, -maxHeightDifference);
			float maxPositiveDelta = Mathf.Min(maxHeight - lastBuilding.height, maxHeightDifference);
			height = lastBuilding.height + UnityEngine.Random.Range(minNegativeDelta, maxPositiveDelta);
		}
		else {
			height = UnityEngine.Random.Range(minHeight, maxHeight);
		}
	
		return height;
	}

	private Color GetNextColor() {
		return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
	}
}
