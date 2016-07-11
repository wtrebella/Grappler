using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PathObjectManager : MonoBehaviour {
	[SerializeField] PathDivisionManager divisionManager;

	// i need to create a custom class instead of using GameObject
	// because i need a blank placeholder, plus sometimes i might want
	// multiple objects in one division.
	// maybe it could be PathDivisionObjects (or something like that)
	private Queue<GameObject> prefabQueue;

	private void Awake() {
		divisionManager.SignalPathDivisionCreated += OnDivisionCreated;
	}
	
	private void Start() {
	
	}
	
	private void Update() {
	
	}

	private void OnDivisionCreated(PathDivision division) {
		
	}

	private GameObject GetNextPrefab() {

	}

	private void AddToPrefabQueue(GameObject prefab, 
}
