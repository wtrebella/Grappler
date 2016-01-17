using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {
	[SerializeField] private string climbingClipName = "Climbing";
	[SerializeField] private string grapplingClipName = "Grappling";
	[SerializeField] private string fallingClipName = "Falling";
	[SerializeField] private string climbingRopeClipName = "Climbing Rope";

	[SerializeField] private tk2dSpriteAnimator bodyAnimator;
	[SerializeField] private tk2dSpriteAnimator feetAnimator;

	public void PlayClimbingAnimations() {
		PlayAnimations(climbingClipName);
	}

	public void PlayGrapplingAnimations() {
		PlayAnimations(grapplingClipName);
	}

	public void PlayFallingAnimations() {
		PlayAnimations(fallingClipName);
	}

	public void PlayClimbingRopeAnimations() {
		PlayAnimations(climbingRopeClipName);
	}

	private void PlayAnimations(string clipName) {
		if (bodyAnimator != null) bodyAnimator.Play(clipName);
		if (feetAnimator != null) bodyAnimator.Play(clipName);
	}
}
