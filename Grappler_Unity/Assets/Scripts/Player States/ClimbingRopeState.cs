using UnityEngine;
using System.Collections;

public class ClimbingRopeState : MonoBehaviour {
	[SerializeField] private SpringJoint2D ropeSpringJoint;
	[SerializeField] private float targetFrequency = 3;
	[SerializeField] private float climbDuration = 1;

	public void ClimbUpRope() {
		TweenSpringJointFrequency();
	}

	private void TweenSpringJointFrequency() {
		Go.to(ropeSpringJoint, climbDuration, new GoTweenConfig().floatProp("frequency", targetFrequency).setUpdateType(GoUpdateType.FixedUpdate).setEaseType(GoEaseType.SineInOut));
	}
}
