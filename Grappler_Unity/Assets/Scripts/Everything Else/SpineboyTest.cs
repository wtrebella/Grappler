using UnityEngine;
using System.Collections;
using Spine;

public class SpineboyTest : MonoBehaviour {
	private SkeletonAnimation animation;

	private void Awake() {
		animation = GetComponent<SkeletonAnimation>();
	}

	private void Start() {
		animation.state.SetAnimation(0, "animation", true);
	}
}
