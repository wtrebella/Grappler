using UnityEngine;
using System.Collections;

public class SpringJointAttributes : ScriptableObject {
	public float distance = 0.005f;
	public float dampingRatio = 0.5f;
	public float frequency = 0.5f;

	public static void ApplyAttributes(SpringJoint2D springJoint, SpringJointAttributes attributes) {
		springJoint.distance = attributes.distance;
		springJoint.dampingRatio = attributes.dampingRatio;
		springJoint.frequency = attributes.frequency;
	}
}
