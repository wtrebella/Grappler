using UnityEngine;
using System.Collections;
using Spine;

public class PlayerAnimator : MonoBehaviour {
	private string clipName_grappling = "grappling";
	private string clipName_falling = "falling";
	private string clipName_onGround = "onGround";
	private string clipName_idle = "idle";
	private string clipName_skating = "idle";// TODO add skating animation

	[SerializeField] private SkeletonAnimation topAnimation;
	[SerializeField] private SkeletonAnimation bottomAnimation;
	
	public void PlayGrapplingAnimations() {
		PlayAnimations(clipName_grappling);
	}

	public void PlayFallingAnimations() {
		PlayAnimations(clipName_falling);
		bottomAnimation.state.SetAnimation(0, clipName_grappling, true);
	}

	public void PlayIdleAnimations() {
		PlayAnimations(clipName_idle);
	}

	public void PlaySkatingAnimations() {
		PlayAnimations(clipName_skating);
	}

	public void PlayOnGroundAnimations() {
		PlayAnimations(clipName_onGround);
	}

	private void PlayAnimations(string clipName) {
		if (topAnimation != null) topAnimation.state.SetAnimation(0, clipName, true);
		if (bottomAnimation != null) bottomAnimation.state.SetAnimation(0, clipName, true);
	}
}
