using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Floor : MonoBehaviour {
	[SerializeField] private float screenMargin = 10;
	[SerializeField] private float tileSize = 2;
	[SerializeField] private Transform floorTilePrefab;

	private List<Transform> floorTiles;

	private void Awake() {
		floorTiles = new List<Transform>();
	}

	private void Start() {
		CreateFloor();
	}

	private void Update() {
		UpdateFloor();
	}

	private float GetMinX() {
		float originX = GameScreen.instance.origin.x;
		float minX = originX - screenMargin;
		return minX;
	}

	private float GetMaxX() {
		float originX = GameScreen.instance.origin.x;
		float maxX = originX + GameScreen.instance.width + screenMargin;
		return maxX;
	}

	private void AddFloorTileToEnd(Vector2 position) {
		Transform floorTile = floorTilePrefab.Spawn();
		floorTile.parent = transform;
		floorTile.position = position;
		floorTiles.Add(floorTile);
	}

	private void AddFloorTileToBeginning(Vector2 position) {
		Transform floorTile = floorTilePrefab.Spawn();
		floorTile.parent = transform;
		floorTile.position = position;
		floorTiles.Insert(0, floorTile);
	}

	private void CreateFloor() {
		float nextX = GetMinX();

		while (nextX < GetMaxX()) {
			Vector2 position = new Vector2(nextX, 0);
			AddFloorTileToEnd(position);
			nextX += tileSize;
		}
	}

	private void UpdateFloor() {
		while (true) {
			Transform floorTile = floorTiles[0];
			if (floorTile.position.x >= GetMinX()) break;

			floorTiles.RemoveAt(0);
			floorTile.Recycle();
			Transform lastFloorTile = floorTiles[floorTiles.Count - 1];
			Vector2 position = new Vector2(lastFloorTile.transform.position.x + tileSize, 0);
			AddFloorTileToEnd(position);
		}

		while (true) {
			Transform floorTile = floorTiles[floorTiles.Count - 1];
			if (floorTile.position.x <= GetMaxX()) break;
			
			floorTiles.RemoveAt(floorTiles.Count - 1);
			floorTile.Recycle();
			Transform firstFloorTile = floorTiles[0];
			Vector2 position = new Vector2(firstFloorTile.transform.position.x - tileSize, 0);
			AddFloorTileToBeginning(position);
		}
	}
}
