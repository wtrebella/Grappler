using UnityEngine;
using System.Collections;
using Spine;

public class PlayerAnimator : MonoBehaviour {
	private string clipName_grappling = "grappling";
	private string clipName_falling = "falling";
	private string clipName_onGround = "idle"; // TODO add on ground animation
	private string clipName_idle = "idle";

	[SerializeField] private SkeletonAnimation skeletonAnimation;

	public void PlayGrapplingAnimations() {
		PlayAnimations(clipName_grappling);
	}

	public void PlayFallingAnimations() {
		PlayAnimations(clipName_falling);
	}

	public void PlayIdleAnimations() {
		PlayAnimations(clipName_idle);
	}

	public void PlayOnGroundAnimations() {
		PlayAnimations(clipName_onGround);
	}

	private void PlayAnimations(string clipName) {
		if (skeletonAnimation != null) skeletonAnimation.state.SetAnimation(0, clipName, true);
	}
}
