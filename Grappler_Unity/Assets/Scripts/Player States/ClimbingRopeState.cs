using UnityEngine;
using System.Collections;

public class ClimbingRopeState : MonoBehaviour {
	public float springJointFrequency {
		get {return ropeSpringJoint.frequency;}
		set {ropeSpringJoint.frequency = value;}
	}

	[SerializeField] private SpringJoint2D ropeSpringJoint;
	[SerializeField] private float targetFrequency = 3;
	[SerializeField] private float climbDuration = 1;

	public void ClimbUpRope() {
		TweenSpringJointFrequency();
	}

	private void TweenSpringJointFrequency() {
		Debug.Log(ropeSpringJoint);
		Go.to(this, climbDuration, new GoTweenConfig().floatProp("springJointFrequency", targetFrequency).setUpdateType(GoUpdateType.FixedUpdate).setEaseType(GoEaseType.SineInOut));
	}
}
