using UnityEngine;
using System.Collections;

public class TerrainLineManager : MonoBehaviour {
	[SerializeField] private TerrainLine terrainLine;
	[SerializeField] private TerrainMesh terrainMesh;

	private void Update() {
		if (!terrainLine.HasSections()) return;
		while (NeedsNewSection()) AddNewSection();
		terrainLine.ClampSectionCount();
	}

	private bool NeedsNewSection() {
		Vector2 lastPoint = terrainLine.GetLastPoint();
		bool needsNew = lastPoint.x < GameScreen.instance.lowerRightWithMargin.x;
		return needsNew;
	}

	private void AddNewSection() {
		terrainLine.AddStraightLine();
		terrainMesh.SetDirty();
	}
}
