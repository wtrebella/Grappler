using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class GameStats : ScriptableObjectSingleton<GameStats> {
	public UnityEvent testEvent;

	public int coinCount = 0;

	public void OnCoinCollected() {
		coinCount++;
		WhitTools.Invoke(testEvent);
	}

	public void OnBackflip() {

	}

	public void OnFrontFlip() {

	}

	public void OnHitGround() {

	}

	public void OnHitMountain() {

	}

	public void OnHitIcicle() {

	}

	public void OnHitTree() {

	}

	public void OnHitRockSlide() {

	}
}
