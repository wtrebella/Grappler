using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CityPointMaker))]
public class Level : MonoBehaviour {
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

	private void CreateAnchorableAtNextPoint() {
		Anchorable anchorable = anchorablePrefab.Spawn();
		anchorable.transform.parent = transform;
		anchorable.transform.position = GetNextAnchorablePosition();
		anchorables.Add(anchorable);
		cityPointMaker.HandleCurrentPointUsed();
	}

	private Vector2 GetNextAnchorablePosition() {
		return cityPointMaker.GetCurrentPoint();
	}

	private bool NextPointIsOnScreenWithMarginX() {
		Vector3 nextPoint = GetNextAnchorablePosition();
		return GameScreen.instance.GetIsOnscreenWithMarginX(nextPoint.x);
	}

	private void CreateAnchorablesIfNeeded() {	
		if (NextPointIsOnScreenWithMarginX()) CreateAnchorableAtNextPoint();
	}

	private void RemoveOffscreenAnchorables() {
		for (int i = 0; i < anchorables.Count; i++) {
			Anchorable anchorable = anchorables[i];
			if (AnchorableIsOnScreenX(anchorable)) break;			
			if (anchorable.isConnected) continue;
			else RecycleAnchorableAtIndex(i);
		}
	}

	private bool AnchorableIsOnScreenX(Anchorable anchorable) {
		float minX = GameScreen.instance.lowerLeftWithMargin.x;
		return anchorable.transform.position.x >= minX;
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
