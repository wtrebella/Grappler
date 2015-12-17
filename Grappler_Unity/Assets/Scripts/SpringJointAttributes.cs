using UnityEngine;
using System.Collections;

public class SpringJointAttributes : ScriptableObject {
	public float dampingRatio = 0.5f;
	public float frequency = 0.5f;

	public static void ApplyAttributes(SpringJoint2D springJoint, SpringJointAttributes attributes) {
		springJoint.dampingRatio = attributes.dampingRatio;
		springJoint.frequency = attributes.frequency;
	}
}
