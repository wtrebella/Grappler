using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(BuildingGenerator))]
public class AnchorableGenerator : MonoBehaviour {
	public Action<Anchorable> SignalAnchorableCreated;

	[SerializeField] private Anchorable anchorablePrefab;
	[SerializeField] private BuildingGenerator buildingGenerator;
	
	private List<Anchorable> anchorables;

	private void Awake() {
		anchorables = new List<Anchorable>();
		buildingGenerator.SignalCreatedBuilding += HandleCreatedBuilding;
	}
	
	private void FixedUpdate() {
		RemoveOffscreenAnchorables();
	}

	private void HandleCreatedBuilding(Building building) {
		Vector2[] cornerPoints = building.GetCornerPoints();
		CreateAnchorableAtPoint(cornerPoints[0]);
		CreateAnchorableAtPoint(cornerPoints[1]);
	}

	private void CreateAnchorableAtPoint(Vector2 position) {
		Anchorable anchorable = anchorablePrefab.Spawn();
		anchorable.transform.parent = transform;
		anchorable.transform.position = position;
		anchorables.Add(anchorable);
		if (SignalAnchorableCreated != null) SignalAnchorableCreated(anchorable);
	}

	private void RemoveOffscreenAnchorables() {
		for (int i = 0; i < anchorables.Count; i++) {
			Anchorable anchorable = anchorables[i];
			if (ShouldRecycleAnchorable(anchorable)) RecycleAnchorableAtIndex(i);
			else break;
		}
	}
	
	private bool ShouldRecycleAnchorable(Anchorable anchorable) {
		return !anchorable.isConnected && GameScreen.instance.IsOffLeftOfScreenWithMargin(anchorable.transform.position.x);
	}

	private void RecycleAnchorable(Anchorable anchorable) {
		int indexOfAnchorable = anchorables.IndexOf(anchorable);
		RecycleAnchorableAtIndex(indexOfAnchorable);
	}
	
	private void RecycleAnchorableAtIndex(int index) {
		WhitTools.Assert(index >= 0 && index < anchorables.Count, "invalid anchorable index");
		
		Anchorable anchorable = anchorables[index];
		anchorables.RemoveAt(index);
		anchorable.Recycle();
	}
}
