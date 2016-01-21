using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {
	[SerializeField] private string grapplingClipName = "Grappling";
	[SerializeField] private string fallingClipName = "Falling";
	[SerializeField] private string kickingClipName = "Kicking";

	[SerializeField] private tk2dSpriteAnimator bodyAnimator;
	[SerializeField] private tk2dSpriteAnimator feetAnimator;
	
	public void PlayGrapplingAnimations() {
		PlayAnimations(grapplingClipName);
	}

	public void PlayFallingAnimations() {
		PlayAnimations(fallingClipName);
	}

	public void PlayKickingAnimations() {
		PlayAnimations(kickingClipName);
	}

	private void PlayAnimations(string clipName) {
		if (bodyAnimator != null) bodyAnimator.Play(clipName);
		if (feetAnimator != null) feetAnimator.Play(clipName);
	}
}
