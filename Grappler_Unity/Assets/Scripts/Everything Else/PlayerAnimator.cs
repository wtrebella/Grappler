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
		bodyAnimator.Play(climbingClipName);
		feetAnimator.Play(climbingClipName);
	}

	public void PlayGrapplingAnimations() {
		bodyAnimator.Play(grapplingClipName);
		feetAnimator.Play(grapplingClipName);
	}

	public void PlayFallingAnimations() {
		bodyAnimator.Play(fallingClipName);
		feetAnimator.Play(fallingClipName);
	}

	public void PlayClimbingRopeAnimations() {
		bodyAnimator.Play(climbingRopeClipName);
		feetAnimator.Play(climbingRopeClipName);
	}
}
