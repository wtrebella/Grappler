using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PathObjectManager : MonoBehaviour {
	[SerializeField] private GameObject[] prefabs;
	[SerializeField] private PathDivisionManager divisionManager;

	private void Awake() {
		divisionManager.SignalPathDivisionCreated += OnDivisionCreated;
	}
	
	private void Start() {
	
	}
	
	private void Update() {
	
	}

	private void OnDivisionCreated(PathDivision division) {
		division.AddObject(prefabs[UnityEngine.Random.Range(0, prefabs.Length)], PathDivisionPositionType.Bottom);
	}
}
