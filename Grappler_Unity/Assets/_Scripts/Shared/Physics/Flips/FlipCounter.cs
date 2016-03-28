using UnityEngine;
using System.Collections;
using System;

public class FlipCounter : MonoBehaviour {
	public Action SignalFrontFlip;
	public Action SignalBackflip;

	[HideInInspector] public int frontFlipCount {get; private set;}
	[HideInInspector] public int backFlipCount {get; private set;}

	[SerializeField] private Rigidbody2D rigid;

	private float cumulativeRotation = 0;
	private float prevRotation = 0;

	private void Awake() {
		frontFlipCount = 0;
		backFlipCount = 0;
	}

	private void FixedUpdate() {
		UpdateRotation();
	}

	private void UpdateRotation() {
		float actualRotation = rigid.transform.eulerAngles.z;
		float curRotation = actualRotation;
		float diff = curRotation - prevRotation;

		while (diff < -200) {
			curRotation += 360;
			diff = curRotation - prevRotation;
		}

		while (diff > 200) {
			curRotation -= 360;
			diff = curRotation - prevRotation;
		}

		cumulativeRotation += diff;
		prevRotation = curRotation;

		if (actualRotation > 300 || actualRotation < 60) {
			if (cumulativeRotation >= 200) {
				cumulativeRotation = 0;
				HandleBackFlip();
			}
			else if (cumulativeRotation <= -200) {
				cumulativeRotation = 0;
				HandleFrontFlip();
			}
		}
	}

	private void HandleFrontFlip() {
		frontFlipCount++;
		if (SignalFrontFlip != null) SignalFrontFlip();
	}


	private void HandleBackFlip() {
		backFlipCount++;
		if (SignalBackflip != null) SignalBackflip();
	}
}
