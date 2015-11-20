using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class BuildingGenerator : MonoBehaviour {
	public Action<Building> SignalCreatedFirstBuilding;
	public Action<Building> SignalCreatedBuilding;

	[SerializeField] private Building buildingPrefab;

	private BuildingAttributeGenerator buildingAttributeGenerator;
	private BuildingAttributes previousBuildingAttributes;
	private List<Building> buildings;

	private void Awake() {
		buildings = new List<Building>();
		buildingAttributeGenerator = GetComponent<BuildingAttributeGenerator>();
	}

	private void Start() {
		CreateBuildingsIfNeeded();
	}

	private void Update() {
		CreateBuildingsIfNeeded();
		RecycleOffScreenBuildings();
	}

	private void CreateBuilding() {
		BuildingAttributes buildingAttributes = GetNextBuildingAttributes();
		CreateBuilding(buildingAttributes);
		previousBuildingAttributes = buildingAttributes;
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

	private BuildingAttributes GetNextBuildingAttributes() {
		return buildingAttributeGenerator.GetRandomBuildingAttributes(previousBuildingAttributes);
	}
}
