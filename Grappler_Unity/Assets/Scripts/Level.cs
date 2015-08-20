using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CityPointMaker))]
public class Level : MonoBehaviour {
	[SerializeField] private Anchorable anchorablePrefab;
	[SerializeField] private SpikeBall spikeBallPrefab;

	private int anchorableCounter = 0;
	private CityPointMaker cityPointMaker;
	private List<Anchorable> anchorables;
	private List<SpikeBall> spikeBalls;

	private void Awake() {
		cityPointMaker = GetComponent<CityPointMaker>();
		anchorables = new List<Anchorable>();
		spikeBalls = new List<SpikeBall>();
	}

	private void Start() {
		CreateInitialAnchorables();
	}

	private void FixedUpdate() {
		CreateAnchorablesIfNeeded();
		CreateSpikeBallIfNeeded();
		RemoveOffscreenAnchorables();
		RemoveOffscreenSpikeBalls();
	}

	private void CreateInitialAnchorables() {
		while (true) {
			if (NextPointIsOnScreenWithMarginX()) CreateAnchorableAtNextPoint();
			else break;
		}
	}

	private void CreateSpikeBallIfNeeded() {
		if (!ShouldCreateSpikeBallThisFrame()) return;

		SpikeBall spikeBall = spikeBallPrefab.Spawn();
		spikeBall.transform.position = GetNewSpikeBallPosition();
		spikeBalls.Add(spikeBall);
	}

	private Vector2 GetNewSpikeBallPosition() {
		return GetLastAnchorable().transform.position;
	}

	private bool ShouldCreateSpikeBallThisFrame() {
		return anchorableCounter % 5 == 0;
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
		anchorableCounter++;
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

	private void RemoveOffscreenSpikeBalls() {
		for (int i = 0; i < spikeBalls.Count; i++) {
			SpikeBall spikeBall = spikeBalls[i];
			if (ShouldRecycleSpikeBall(spikeBall)) RecycleSpikeBallAtIndex(i);
			else break;
		}
	}

	private bool ShouldRecycleAnchorable(Anchorable anchorable) {
		return !anchorable.isConnected && GameScreen.instance.IsOffLeftOfScreenWithMargin(anchorable.transform.position.x);
	}

	private bool ShouldRecycleSpikeBall(SpikeBall spikeBall) {
		return GameScreen.instance.IsOffLeftOfScreenWithMargin(spikeBall.transform.position.x);
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

	private void RecycleSpikeBall(SpikeBall spikeBall) {
		int indexOfSpikeBall = spikeBalls.IndexOf(spikeBall);
		RecycleAnchorableAtIndex(indexOfSpikeBall);
	}
	
	private void RecycleSpikeBallAtIndex(int index) {
		WhitTools.Assert(index >= 0 && index < spikeBalls.Count, "invalid spikeBall index");
		
		SpikeBall spikeBall = spikeBalls[index];
		spikeBalls.RemoveAt(index);
		spikeBall.Recycle();
	}
}
