using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TerrainChunk : MonoBehaviour {
	public Transform exitPoint;
	
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
