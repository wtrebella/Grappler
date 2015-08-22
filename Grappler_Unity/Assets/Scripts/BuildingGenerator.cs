using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class BuildingGenerator : MonoBehaviour {
	public Action<Building> SignalCreatedBuilding;

	[SerializeField] Building buildingPrefab;
	[SerializeField] float minWidth = 5;
	[SerializeField] float maxWidth = 30;
	[SerializeField] float minHeight = 50;
	[SerializeField] float maxHeight = 100;
	[SerializeField] float maxHeightDifference = 50;

	private List<Building> buildings;
	private Vector2 previousSize = Vector2.zero;
	private float previousX = 0;
	private int buildingCounter = 0;

	private void Awake() {
		buildings = new List<Building>();
	}

	private void Start() {
		CreateBuildings(100);
	}

	private void CreateBuildings(int numBuildings) {
		for (int i = 0; i < numBuildings; i++) CreateRandomBuilding();
	}

	private void CreateRandomBuilding() {
		Vector2 size = GetNextSize();
		Color color = GetNextColor();
		float x = previousX + previousSize.x;
		Vector2 position = new Vector2(x, 0);
		
		BuildingAttributes buildingAttributes = new BuildingAttributes();
		buildingAttributes.size = size;
		buildingAttributes.color = color;
		buildingAttributes.position = position;
		
		CreateBuilding(buildingAttributes);
	}
	
	private void CreateBuilding(BuildingAttributes buildingAttributes) {
		Building building = Instantiate(buildingPrefab);
		building.transform.parent = transform;
		building.SetBuildingAttributes(buildingAttributes);
		previousX = buildingAttributes.position.x;
		previousSize = buildingAttributes.size;
		buildings.Add(building);
		if (SignalCreatedBuilding != null) SignalCreatedBuilding(building);

		buildingCounter++;
	}

	private Vector2 GetNextSize() {
		float width = GetNextWidth();
		float height = GetNextHeight();
		
		return new Vector2(width, height);
	}

	private float GetNextWidth() {
		return UnityEngine.Random.Range(minWidth, maxWidth);
	}

	private float GetNextHeight() {
		float height = 0;

		if (buildingCounter == 0) {
			height = UnityEngine.Random.Range(minHeight, maxHeight);
		}
		else {
			float minNegativeDelta = Mathf.Max(minHeight - previousSize.y, -maxHeightDifference);
			float maxPositiveDelta = Mathf.Min(maxHeight - previousSize.y, maxHeightDifference);
			height = previousSize.y + UnityEngine.Random.Range(minNegativeDelta, maxPositiveDelta);
		}
	
		return height;
	}

	private Color GetNextColor() {
		return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
	}
}
