using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpikeBallGenerator : MonoBehaviour {
	[SerializeField] private OldAnchorableGenerator anchorableGenerator;
	[SerializeField] private SpikeBall spikeBallPrefab;

	private int anchorableCounter = 0;
	private List<SpikeBall> spikeBalls;
	
	private void Awake() {
		spikeBalls = new List<SpikeBall>();
		anchorableGenerator.SignalAnchorableCreated += HandleAnchorableCreated;
	}
	
	private void FixedUpdate() {
		RemoveOffscreenSpikeBalls();
	}
	
	private void CreateSpikeBall(Anchorable nearbyAnchorable) {
		SpikeBall spikeBall = spikeBallPrefab.Spawn();
		spikeBall.transform.position = GetNewSpikeBallPosition(nearbyAnchorable);
		spikeBalls.Add(spikeBall);
	}
	
	private Vector2 GetNewSpikeBallPosition(Anchorable anchorable) {
		Vector2 anchorablePosition = anchorable.transform.position;
		Vector2 delta = Random.insideUnitCircle * 5;
		Vector2 spikeBallPosition = anchorablePosition + delta;
		return spikeBallPosition;
	}
	
	private bool ShouldCreateSpikeBall() {
		return anchorableCounter % 5 == 0;
	}
	
	private void HandleAnchorableCreated(Anchorable anchorable) {
		anchorableCounter++;
		if (ShouldCreateSpikeBall()) CreateSpikeBall(anchorable);
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
	
	private void RecycleSpikeBall(SpikeBall spikeBall) {
		int indexOfSpikeBall = spikeBalls.IndexOf(spikeBall);
		RecycleSpikeBallAtIndex(indexOfSpikeBall);
	}
	
	private void RecycleSpikeBallAtIndex(int index) {
		WhitTools.Assert(index >= 0 && index < spikeBalls.Count, "invalid spikeBall index");
		
		SpikeBall spikeBall = spikeBalls[index];
		spikeBalls.RemoveAt(index);
		spikeBall.Recycle();
	}
}
