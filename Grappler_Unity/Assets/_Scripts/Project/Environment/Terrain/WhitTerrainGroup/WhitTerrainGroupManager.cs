using UnityEngine;
using System.Collections;

public class WhitTerrainGroupManager : MonoBehaviour {
	[SerializeField] private WhitTerrainGroup terrainGroup;

	public bool shouldAddStraight = false;
	public bool shouldAddBump = false;

	private void Awake() {
		
	}

	private void Update() {
		if (shouldAddStraight) AddStraight();
		if (shouldAddBump) AddBump();
	}

	private void AddStraight() {
		shouldAddStraight = false;

		terrainGroup.AddStraight(0, 30);
	}

	private void AddBump() {
		shouldAddBump = false;

		terrainGroup.AddCurveThenStraight(0.5f, 20.0f);
		terrainGroup.AddCurveThenStraight(-0.5f, 20.0f);
		terrainGroup.AddCurveThenStraight(0, 20.0f);
	}
}
