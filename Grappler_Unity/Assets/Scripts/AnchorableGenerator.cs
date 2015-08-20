using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(CityPointMaker))]
public class AnchorableGenerator : MonoBehaviour {
	public Action<Anchorable> SignalAnchorableCreated;
	[SerializeField] private Anchorable anchorablePrefab;

	private CityPointMaker cityPointMaker;
	private List<Anchorable> anchorables;

	private void Awake() {
		cityPointMaker = GetComponent<CityPointMaker>();
		anchorables = new List<Anchorable>();
	}
	
	private void Start() {
		CreateInitialAnchorables();
	}
	
	private void FixedUpdate() {
		CreateAnchorablesIfNeeded();
		RemoveOffscreenAnchorables();
	}
	
	private void CreateInitialAnchorables() {
		while (true) {
			if (NextPointIsOnScreenWithMarginX()) CreateAnchorableAtNextPoint();
			else break;
		}
	}
	
	private Anchorable GetLastAnchorable() {
		return anchorables[anchorables.Count - 1];
	}
	
	private void CreateAnchorableAtNextPoint() {
		Anchorable anchorable = anchorablePrefab.Spawn();
		anchorable.transform.parent = transform;
		anchorable.transform.position = GetNextAnchorablePosition();
		anchorables.Add(anchorable);
		cityPointMaker.HandleCurrentPointUsed();
		if (SignalAnchorableCreated != null) SignalAnchorableCreated(anchorable);
	}
	
	private Vector2 GetNextAnchorablePosition() {
		return cityPointMaker.GetCurrentPoint();
	}
	
	private bool NextPointIsOnScreenWithMarginX() {
		Vector3 nextPoint = GetNextAnchorablePosition();
		return GameScreen.instance.IsOnscreenHorizontallyWithMargin(nextPoint.x);
	}
	
	private void CreateAnchorablesIfNeeded() {	
		if (NextPointIsOnScreenWithMarginX()) CreateAnchorableAtNextPoint();
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
