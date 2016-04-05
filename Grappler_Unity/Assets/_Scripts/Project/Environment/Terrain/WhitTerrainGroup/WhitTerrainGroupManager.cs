using UnityEngine;
using System.Collections;

public class WhitTerrainGroupManager : MonoBehaviour {
	[SerializeField] private WhitTerrainPair terrainGroup;

//	public bool shouldAddStraight = false;
//	public bool shouldAddBump = false;
//
//	private void Awake() {
//		
//	}
//
//	private void Update() {
//		if (shouldAddStraight) AddStraight();
//		if (shouldAddBump) AddBumps();
//	}
//
//	private void AddStraight() {
//		shouldAddStraight = false;
//
//		terrainGroup.AddStraight(0, 30);
//	}
//
//	private void AddBumps() {
//		shouldAddBump = false;
//
//		terrainGroup.AddCurve(0.5f);
//		terrainGroup.AddCurve(0);
//		terrainGroup.AddCurve(-0.5f);
//		terrainGroup.AddCurve(0);
//	}
}
