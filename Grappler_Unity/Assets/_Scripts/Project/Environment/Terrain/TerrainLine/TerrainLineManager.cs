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
		// TODO you need to make an overall terrain controller that actually controls
		// both ground AND terrain together, with preset patterns and slopes and curves
		// and shit like that.
		terrainLine.AddStraightLine(Random.Range(0.0f, 0.2f));
		terrainMesh.SetDirty();
	}
}
