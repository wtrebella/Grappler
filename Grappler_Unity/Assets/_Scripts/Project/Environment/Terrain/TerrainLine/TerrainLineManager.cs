using UnityEngine;
using System.Collections;

public class TerrainLineManager : MonoBehaviour {
	[SerializeField] private TerrainLine terrainLine;

	private void Update() {
		if (!terrainLine.HasSections()) return;
		if (NeedsNewSection()) AddNewSection();
	}

	private bool NeedsNewSection() {
		Vector2 lastPoint = terrainLine.GetLastPoint();
		return GameScreen.instance.IsOnscreenHorizontallyWithMargin(lastPoint.x);
	}

	private void AddNewSection() {
		terrainLine.AddStraightLine();
	}
}
