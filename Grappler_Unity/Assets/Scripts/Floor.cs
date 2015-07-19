using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Floor : MonoBehaviour {
	[SerializeField] private float tileSize = 2;
	[SerializeField] private Transform floorTilePrefab;

	private List<Transform> floorTiles;

	private void Awake() {
		floorTiles = new List<Transform>();
	}

	private void Start() {
		CreateFloor();
	}

	private void FixedUpdate() {
		MoveOffscreenLeftTilesToEnd();
		MoveOffscreenRightTilesToBeginning();
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
		float nextX = GameScreen.instance.lowerLeftWithMargin.x;
		float maxX = GameScreen.instance.upperRightWithMargin.x;

		while (nextX < maxX) {
			Vector2 position = new Vector2(nextX, 0);
			AddFloorTileToEnd(position);
			nextX += tileSize;
		}
	}

	private void MoveOffscreenLeftTilesToEnd() {
		float minX = GameScreen.instance.lowerLeftWithMargin.x;
		
		while (true) {
			Transform floorTile = floorTiles[0];
			if (floorTile.position.x >= minX) break;
			
			floorTiles.RemoveAt(0);
			floorTile.Recycle();
			Transform lastFloorTile = floorTiles[floorTiles.Count - 1];
			Vector2 position = new Vector2(lastFloorTile.transform.position.x + tileSize, 0);
			AddFloorTileToEnd(position);
		}
	}

	private void MoveOffscreenRightTilesToBeginning() {
		float maxX = GameScreen.instance.upperRightWithMargin.x;
		
		while (true) {
			Transform floorTile = floorTiles[floorTiles.Count - 1];
			if (floorTile.position.x <= maxX) break;
			
			floorTiles.RemoveAt(floorTiles.Count - 1);
			floorTile.Recycle();
			Transform firstFloorTile = floorTiles[0];
			Vector2 position = new Vector2(firstFloorTile.transform.position.x - tileSize, 0);
			AddFloorTileToBeginning(position);
		}
	}
}
