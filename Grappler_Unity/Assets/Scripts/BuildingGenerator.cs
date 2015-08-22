using UnityEngine;
using System.Collections;

public class BuildingGenerator : MonoBehaviour {
	[SerializeField] Building buildingPrefab;
	[SerializeField] float minWidth = 5;
	[SerializeField] float maxWidth = 30;
	[SerializeField] float minHeight = 50;
	[SerializeField] float maxHeight = 100;

	private void Start() {
		CreateBuildings();
	}

	private void CreateBuildings() {
		float x = 0;
		
		for (int i = 0; i < 100; i++) {
			Vector2 size = GetRandomSize();
			Color color = GetRandomColor();
			Vector2 position = new Vector2(x, 0);

			BuildingAttributes buildingAttributes = new BuildingAttributes();
			buildingAttributes.size = size;
			buildingAttributes.color = color;
			buildingAttributes.position = position;

			CreateBuilding(buildingAttributes);

			x += size.x;
		}
	}

	private Vector2 GetRandomSize() {
		return new Vector2(Random.Range(minWidth, maxWidth), Random.Range(minHeight, maxHeight));
	}

	private Color GetRandomColor() {
		return new Color(Random.value, Random.value, Random.value);
	}

	private void CreateBuilding(BuildingAttributes buildingAttributes) {
		Building building = Instantiate(buildingPrefab);
		building.SetColor(buildingAttributes.color);
		building.SetSize(buildingAttributes.size);
		building.transform.position = buildingAttributes.position;
	}
}
