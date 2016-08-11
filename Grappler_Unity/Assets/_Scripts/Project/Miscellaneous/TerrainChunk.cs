using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Polydraw;

public class TerrainChunk : MonoBehaviour {
	public Transform exitPoint;

	public bool hasCeiling {get {return ceilings.Length > 0;}}
	public bool hasFloor {get {return floors.Length > 0;}}

	[SerializeField] private PolydrawObject[] ceilings;
	[SerializeField] private PolydrawObject[] floors;

	public PolydrawObject[] GetCeilings() {
		if (!hasCeiling) Debug.LogWarning("Doesn't have ceiling. Returning empty array.");
		return ceilings;
	}

	public PolydrawObject[] GetFloors() {
		if (!hasFloor) Debug.LogWarning("Doesn't have floor. Returning empty array.");
		return floors;
	}

	public PolydrawObject GetFirstFloor() {
		return GetFloors()[0];
	}

	public PolydrawObject GetFirstCeiling() {
		return GetCeilings()[0];
	}

	private void Awake() {
		CreateExitTrigger();
	}

	private void CreateExitTrigger() {
		GameObject go = new GameObject("Exit Trigger");
		BoxCollider2D exitTrigger = go.AddComponent<BoxCollider2D>();
		exitTrigger.isTrigger = true;
		exitTrigger.offset = new Vector2(-15, 0);
		exitTrigger.size = new Vector2(30, 300);
		exitTrigger.transform.SetParent(exitPoint);
		exitTrigger.transform.localPosition = Vector2.zero;
		exitTrigger.gameObject.layer = LayerMask.NameToLayer("Exit");
	}
	
	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
