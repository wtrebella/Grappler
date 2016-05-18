using UnityEngine;
using System.Collections;

public class FlipManager : MonoBehaviour {
	public int flipCount {get; private set;}

	[SerializeField] private FlipCounter flipCounter;

	private void Awake() {
		flipCount = 0;
		flipCounter.SignalBackflip += OnFlip;
		flipCounter.SignalFrontFlip += OnFlip;
	}

	private void OnFlip() {
		flipCount++;
	}

	public void ResetFlipCount() {
		flipCount = 0;
	}
}